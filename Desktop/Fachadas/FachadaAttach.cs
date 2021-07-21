using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;

using PurchaseData.DataModel;

using PurchaseDesktop.Interfaces;

using static PurchaseDesktop.Helpers.Enums;

namespace PurchaseDesktop.Fachadas
{
    public class FachadaAttach
    {
        public EPerfiles CurrentPerfil { get; set; }
        public IPerfilActions PerfilActions { get; set; }
        public ConfigApp ConfigApp { get; set; }
        public TextInfo UCase { get; set; } = CultureInfo.InvariantCulture.TextInfo;

        public FachadaAttach(IPerfilActions perfilActions, ConfigApp configApp)
        {
            PerfilActions = perfilActions;
            Enum.TryParse(perfilActions.CurrentUser.ProfileID, out EPerfiles p);
            CurrentPerfil = p;
            ConfigApp = configApp;
        }

        #region Attach CRUD

        public string InsertAttach(Attaches item, DataRow headerDR, string path)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            if (!Directory.Exists(ConfigApp.FolderApp + headerID))
            {
                Directory.CreateDirectory(ConfigApp.FolderApp + headerID);
            }
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    if (File.Exists($"{ConfigApp.FolderApp}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    PerfilActions.InsertAttach<OrderHeader>(item, po);
                    //! Copiar el archivo en la carpeta
                    CopyFile(path, $"{ConfigApp.FolderApp}{headerID}{item.FileName}");
                    break;
                case EPerfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Requisition is not allowed."; }
                    if (File.Exists($"{ConfigApp.FolderApp}{headerID}{item.FileName}"))
                    {
                        return "The file you are trying to copy already exists on the server.";
                    }
                    PerfilActions.InsertAttach<RequisitionHeader>(item, pr);
                    //! Copiar el archivo en la carpeta
                    CopyFile(path, $"{ConfigApp.FolderApp}{headerID}{item.FileName}");
                    break;
                case EPerfiles.VAL:
                    break;
                default:
                    break;
            }
            return "OK";
        }

        private void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void CopyFile(string source, string target)
        {
            try
            {
                File.Copy(source, target, true); //! True: sobre escribir el file
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteAttach(DataRow attachDR, DataRow headerDR)
        {
            var headerID = Convert.ToInt32(headerDR["headerID"]);
            var attachID = Convert.ToInt32(attachDR["attachID"]);
            Enum.TryParse(headerDR["TypeDocumentHeader"].ToString(), out TypeDocumentHeader td);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            return "Your profile does not allow you to complete this action.";
                        case TypeDocumentHeader.PO:
                            var po = new OrderHeader().GetById(headerID);
                            if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            //! Borrar el archivo de la carpeta
                            DeleteFile($"{ConfigApp.FolderApp}{headerID}{attachDR["FileName"]}");
                            PerfilActions.DeleteAttach(po, attachID);
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    switch (td)
                    {
                        case TypeDocumentHeader.PR:
                            //todo Saber si tiene acceso a la carpeta en el server: ds
                            // DirectorySecurity ds = Directory.GetAccessControl(configApp.FolderApp);
                            var pr = new RequisitionHeader().GetById(headerID);
                            if (pr.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                            //! Borrar el archivo de la carpeta
                            DeleteFile($"{ConfigApp.FolderApp}{headerID}{attachDR["FileName"]}");
                            PerfilActions.DeleteAttach<RequisitionHeader>(pr, attachID);
                            break;
                        case TypeDocumentHeader.PO:
                            return "Your profile does not allow you to complete this action.";
                    }
                    break;
                case EPerfiles.VAL:
                    break;
            }
            return "OK";
        }

        public string OpenAttach(DataRow attachDR, DataRow headerDR)
        {
            try
            {
                var headerID = Convert.ToInt32(headerDR["headerID"]);
                Process.Start($"{ConfigApp.FolderApp}{headerID}{attachDR["FileName"]}");
                return "OK";
            }
            catch (Exception)
            {
                return "The system cannot find the requested path.";
            }
        }

        public string UpdateAttach(object newValue, DataRow attachDR, DataRow headerDR, string campo)
        {
            var headerID = Convert.ToInt32(headerDR["HeaderID"]);
            var attachID = Convert.ToInt32(attachDR["AttachID"]);
            var att = new Attaches().GetByID(attachID);
            switch (CurrentPerfil)
            {
                case EPerfiles.ADM:
                    break;
                case EPerfiles.BAS:
                    break;
                case EPerfiles.UPO:
                    var po = new OrderHeader().GetById(headerID);
                    if (po.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    switch (campo)
                    {
                        case "Description":
                            att.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            PerfilActions.UpdateAttaches<OrderHeader>(att, po);
                            break;
                        case "Modifier":
                            att.Modifier = Convert.ToByte(newValue);
                            PerfilActions.UpdateAttaches<OrderHeader>(att, po);
                            break;
                    }
                    break;
                case EPerfiles.UPR:
                    var pr = new RequisitionHeader().GetById(headerID);
                    if (pr.StatusID >= 2) { return "The 'status' of the Purchase Order is not allowed."; }
                    //var attt = pr.Attaches.FirstOrDefault(c => c.AttachID == attachID);
                    switch (campo)
                    {
                        case "Description":
                            att.Description = UCase.ToTitleCase(newValue.ToString().ToLower());
                            PerfilActions.UpdateAttaches<RequisitionHeader>(att, pr);
                            break;
                        case "Modifier":
                            att.Modifier = Convert.ToByte(newValue);
                            PerfilActions.UpdateAttaches<RequisitionHeader>(att, pr);
                            break;
                    }
                    break;
                case EPerfiles.VAL:
                    break;
            }
            return "OK";

        }
        #endregion
    }
}
