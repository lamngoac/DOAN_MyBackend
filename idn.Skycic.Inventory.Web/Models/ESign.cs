using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Web;
using System.Xml;
using idn.Skycic.Inventory.Utils;
using wSigner;

namespace idn.Skycic.Inventory.Web.Models
{
    public class ESign
    {
        public static string[] listType = { "0", "1", "2", "3" };
        public static string FilePathPfx = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", "Certificate", "idocNet_TVAN", ".pfx"));
        //public static string certPassword = "123123123";
        

        public static bool Sign(string keyType, object filepathpfx, object certpassword, ref string serialNumber, string content, ref string contentsign, ref string errorMessage, ref string certdata)
        {
            try
            {
                contentsign = "";
                if (listType.Contains(keyType) == false)
                {
                    errorMessage = "Loại chứng từ không hợp lệ. Loại chứng từ phải thuộc:\r\n";
                    errorMessage += " + 0: Ký chứng từ XML.\r\n";
                    return false;
                }
                X509Certificate2 cert;
                //FilePathLocal
                filepathpfx = "D:\\AllPrjs\\idocNet\\2018.5.TVAN\\Dev\\V22.Dev\\idn.Skycic.Inventory.Web\\Certificate\\idocNet_TVAN.pfx";
                cert = FindCertificateByPFX(filepathpfx.ToString(), certpassword.ToString());
                if (cert == null)
                {
                    errorMessage = "Chữ ký số không tồn tại.";
                    return false;
                }

                serialNumber = cert.SerialNumber;
                certdata = Convert.ToBase64String(cert.RawData);
                string error = string.Empty;
                if (keyType == "0")
                {
                    var contentEncode = CUtils.XML_HttpUtility_HtmlEncode(content);
                    var contentDecode = CUtils.XML_HttpUtility_HtmlDecode(contentEncode);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(contentDecode);
                    error = SignXml_TVAN(xmlDoc, ref contentsign, cert);
                    if (error != string.Empty)
                    {
                        errorMessage = "Phát sinh lỗi: " + error;
                        return false;
                    }
                    return true;
                }
                else
                {
                    errorMessage = "Dữ liệu cần kí không đúng định dạng XML";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + "\r\n" + ex.StackTrace;
                return false;
            }
        }

        public static bool SignTCT(string keyType, object filepathpfx, object certpassword, ref string serialNumber, string content, ref string contentsign, ref string errorMessage, ref string certdata)
        {
            try
            {
                contentsign = "";
                if (listType.Contains(keyType) == false)
                {
                    errorMessage = "Loại chứng từ không hợp lệ. Loại chứng từ phải thuộc:\r\n";
                    errorMessage += " + 0: Ký chứng từ XML.\r\n";
                    return false;
                }
                X509Certificate2 cert;
                filepathpfx = "D:\\AllPrjs\\idocNet\\2018.5.TVAN\\Dev\\V22.Dev\\idn.Skycic.Inventory.Web\\Certificate\\idocNet_TVAN.pfx";
                //filepathpfx = "E:\\AllPrjs\\idocNet\\2018.5.TVAN\\Dev\\V22.Dev\\idn.Skycic.Inventory.Web\\Certificate\\idocNet_TVAN.pfx";
                cert = FindCertificateByPFX(filepathpfx.ToString(), certpassword.ToString());
                if (cert == null)
                {
                    errorMessage = "Chữ ký số không tồn tại.";
                    return false;
                }

                serialNumber = cert.SerialNumber;
                certdata = Convert.ToBase64String(cert.RawData);
                string error = string.Empty;
                if (keyType == "0")
                {
                    var contentEncode = CUtils.XML_HttpUtility_HtmlEncode(content);
                    var contentDecode = CUtils.XML_HttpUtility_HtmlDecode(contentEncode);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(contentDecode);
                    error = SignXml_TVAN_TCT(xmlDoc, ref contentsign, cert);
                    if (error != string.Empty)
                    {
                        errorMessage = "Phát sinh lỗi: " + error;
                        return false;
                    }
                    return true;
                }
                else
                {
                    errorMessage = "Dữ liệu cần kí không đúng định dạng XML";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + "\r\n" + ex.StackTrace;
                return false;
            }
        }

        public static string SignXml_TVAN(XmlDocument xmlDoc, ref string result, X509Certificate2 cert)
        {
            bool isTKDangKy = false;
            try
            {
                if (cert.PrivateKey == null)
                {
                    return "Người dùng chưa cắm CKS.";
                }
                RSA key = (RSACryptoServiceProvider)cert.PrivateKey;

                var idNode = GetXmlNode(xmlDoc, "http://kekhaithue.gdt.gov.vn/TKhaiThue", "HSoThueDTu/HSoKhaiThue");
                if (idNode == null)
                {
                    //idNode = GetXmlNode(xmlDoc, "http://kekhaithue.gdt.gov.vn/HSoDKy", "DKyThueDTu/DKyThue");
                    idNode = GetXmlNode(xmlDoc, "http://kekhaithue.gdt.gov.vn/HSoDKy", "Invoice/InvoiceTemplate");
                    isTKDangKy = true;
                }
                xmlDoc.PreserveWhitespace = true;
                SignedXml signedXml = new SignedXml(xmlDoc);
                signedXml.SigningKey = key;
                //Signature XMLSignature = signedXml.Signature;
                System.Security.Cryptography.Xml.Signature XMLSignature = signedXml.Signature;

                System.Security.Cryptography.Xml.Reference reference = new System.Security.Cryptography.Xml.Reference();
                if (idNode.Attributes.Count == 0)
                {
                    XmlNode attr = xmlDoc.CreateNode(XmlNodeType.Attribute, "id", "");
                    attr.Value = "ID1";
                    idNode.Attributes.SetNamedItem(attr);
                }
                reference.Uri = "#" + idNode.Attributes[0].Value;

                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);

                XMLSignature.SignedInfo.AddReference(reference);

                System.Security.Cryptography.Xml.KeyInfo keyInfo = new System.Security.Cryptography.Xml.KeyInfo();
                keyInfo.AddClause(new System.Security.Cryptography.Xml.RSAKeyValue((RSA)key));

                var c = new KeyInfoX509Data(cert);
                c.AddSubjectName(cert.Subject);
                keyInfo.AddClause(c);

                XMLSignature.KeyInfo = keyInfo;

                signedXml.ComputeSignature();

                XmlElement xmlDigital = signedXml.GetXml();
                XmlNamespaceManager man = new XmlNamespaceManager(xmlDoc.NameTable);
                if (isTKDangKy == false) man.AddNamespace("HoSo", "http://kekhaithue.gdt.gov.vn/TKhaiThue");
                else man.AddNamespace("HoSo", "http://kekhaithue.gdt.gov.vn/HSoDKy");
                var nodeCKy = xmlDoc.GetElementsByTagName("CKyDTu");
                if (nodeCKy.Count == 0)
                {
                    if (isTKDangKy == false)
                    {
                        var nodeP = xmlDoc.GetElementsByTagName("HSoThueDTu")[0];
                        XmlNode cKy = xmlDoc.CreateElement("CKyDTu", nodeP.NamespaceURI);
                        cKy.InnerXml = "";
                        xmlDoc.GetElementsByTagName("HSoThueDTu")[0].AppendChild(cKy);
                        nodeCKy = xmlDoc.GetElementsByTagName("CKyDTu");
                    }
                    else
                    {
                        //var nodeP = xmlDoc.GetElementsByTagName("DKyThueDTu")[0];
                        //XmlNode cKy = xmlDoc.CreateElement("CKyDTu", nodeP.NamespaceURI);
                        //cKy.InnerXml = "";
                        //xmlDoc.GetElementsByTagName("DKyThueDTu")[0].AppendChild(cKy);
                        //nodeCKy = xmlDoc.GetElementsByTagName("CKyDTu");

                        var nodeP = xmlDoc.GetElementsByTagName("Invoice")[0];
                        XmlNode cKy = xmlDoc.CreateElement("CKyDTu", nodeP.NamespaceURI);
                        cKy.InnerXml = "";
                        xmlDoc.GetElementsByTagName("Invoice")[0].AppendChild(cKy);
                        nodeCKy = xmlDoc.GetElementsByTagName("CKyDTu");
                    }

                }
                nodeCKy[0].AppendChild(xmlDigital);
                result = CUtils.XML_Unescape_Value(xmlDoc.OuterXml);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message + "\r\n==========\r\n" + ex.StackTrace;
            }
        }

        public static string SignXml_TVAN_TCT(XmlDocument xmlDoc, ref string result, X509Certificate2 cert)
        {
            bool isTKDangKy = false;
            try
            {
                if (cert.PrivateKey == null)
                {
                    return "Người dùng chưa cắm CKS.";
                }
                RSA key = (RSACryptoServiceProvider)cert.PrivateKey;
                var idNode = GetXmlNode(xmlDoc, "http://kekhaithue.gdt.gov.vn/TKhaiThue", "HSoThueDTu/HSoKhaiThue");
                if (idNode == null)
                {
                    //idNode = GetXmlNode(xmlDoc, "http://kekhaithue.gdt.gov.vn/HSoDKy", "DKyThueDTu/DKyThue");
                    //idNode = GetXmlNode(xmlDoc, "http://kekhaithue.gdt.gov.vn/HSoDKy", "DATA/HEADER");
                    idNode = GetXmlNode(xmlDoc, "DATA", "DATA/HEADER");
                    isTKDangKy = true;
                    if (idNode == null)
                    {
                        idNode = xmlDoc.SelectSingleNode("DATA/HEADER");
                    }
                }
                xmlDoc.PreserveWhitespace = true;
                SignedXml signedXml = new SignedXml(xmlDoc) { SigningKey = key };
                System.Security.Cryptography.Xml.Signature XMLSignature = signedXml.Signature;
                System.Security.Cryptography.Xml.Reference reference = new System.Security.Cryptography.Xml.Reference();
                if (idNode.Attributes.Count == 0)
                {
                    XmlNode attr = xmlDoc.CreateNode(XmlNodeType.Attribute, "id", "");
                    attr.Value = "ID1";
                    idNode.Attributes.SetNamedItem(attr);
                }
                reference.Uri = "#" + idNode.Attributes[0].Value;


                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);
                XMLSignature.SignedInfo.AddReference(reference);
                System.Security.Cryptography.Xml.KeyInfo keyInfo = new System.Security.Cryptography.Xml.KeyInfo();
                keyInfo.AddClause(new System.Security.Cryptography.Xml.RSAKeyValue((RSA)key));
                var c = new KeyInfoX509Data(cert);
                c.AddSubjectName(cert.Subject);
                keyInfo.AddClause(c);
                XMLSignature.KeyInfo = keyInfo;
                signedXml.ComputeSignature();
                XmlElement xmlDigital = signedXml.GetXml();

                var nodeCKy = xmlDoc.GetElementsByTagName("SECURITY");
                if (nodeCKy.Count == 0)
                {
                    var nodeP = xmlDoc.GetElementsByTagName("DATA")[0];
                    XmlNode cKy = xmlDoc.CreateElement("SECURITY", nodeP.NamespaceURI);
                    cKy.InnerXml = "";
                    xmlDoc.GetElementsByTagName("DATA")[0].AppendChild(cKy);
                    nodeCKy = xmlDoc.GetElementsByTagName("SECURITY");
                }
                nodeCKy[0].AppendChild(xmlDigital);
                result = CUtils.XML_Unescape_Value(xmlDoc.OuterXml);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message + "\r\n==========\r\n" + ex.StackTrace;
            }
        }

        private static XmlNode GetXmlNode(XmlDocument xmlDoc, string name, string path)
        {
            XmlNamespaceManager man = new XmlNamespaceManager(xmlDoc.NameTable);
            man.AddNamespace("HoSo", name);
            if (!path.StartsWith("/")) path = "HoSo:" + path;
            return xmlDoc.SelectSingleNode(path.Replace("/", "/HoSo:"), man);
        }

        public static X509Certificate2 FindCertificate(string serialNumber)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var list = store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, false);
                if (list.Count > 0) return list[0];
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                store.Close();
            }
        }

        public static X509Certificate2 FindCertificateByPFX(string fileName, string password)
        {
            var x509Certificate2 = CertUtil.GetFromFile(fileName, password);
            return x509Certificate2;
        }

        public static X509Certificate2 GetCertificate()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var list = X509Certificate2UI.SelectFromCollection(store.Certificates, "Chọn chữ ký số", "Chọn một chữ ký số để ký chứng từ", X509SelectionFlag.SingleSelection);
                if (list.Count > 0) return list[0];
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                store.Close();
            }
        }
    }
}