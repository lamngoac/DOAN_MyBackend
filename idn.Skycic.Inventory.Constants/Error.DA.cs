using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Errors
{
    public partial class ErrDA
    {
        #region // Seq
        public const string Seq_Common_MyGet_InvalidSequenceType = "Err.DA.Seq_Common_MyGet_InvalidSequenceType";

        public const string Seq_Common_Get = "Seq_Common_Get";
        public const string WAS_Seq_Common_Get = "WAS_Seq_Common_Get";
        #endregion

        #region // Sys_User
        public const string Sys_User_CheckDB_UserCodeNotFound = "Err.DA.Sys_User_CheckDB_UserCodeNotFound";
        public const string Sys_User_CheckDB_UserCodeExist = "Err.DA.Sys_User_CheckDB_UserCodeExist";
        public const string Sys_User_CheckDB_FlagActiveNotMatched = "Err.DA.Sys_User_CheckDB_FlagActiveNotMatched";

        public const string Sys_User_Login = "Err.DA.Sys_User_Login";
        public const string WAS_Sys_User_Login = "Err.DA.WAS_Sys_User_Login";
        public const string Sys_User_Login_InvalidFlagBG = "Err.DA.Sys_User_Login_InvalidFlagBG";
        public const string Sys_User_Login_InvalidPassword = "Tài khoản hoặc mật khẩu không chính xác"; //"Err.DA.Sys_User_Login_InvalidPassword";

        public const string Sys_User_ChangePassword = "Err.DA.Sys_User_ChangePassword";
        public const string WAS_Sys_User_ChangePassword = "Err.DA.WAS_Sys_User_ChangePassword";
        public const string Sys_User_ChangePassword_InvalidPasswordOld = "Err.DA.Sys_User_ChangePassword_InvalidPasswordOld";
        public const string Sys_User_ChangePassword_InvalidPasswordNew = "Err.DA.Sys_User_ChangePassword_InvalidPasswordNew";

        public const string Sys_User_GetForCurrentUser = "Err.DA.Sys_User_GetForCurrentUser";
        public const string WAS_Sys_User_GetForCurrentUser = "Err.DA.WAS_Sys_User_GetForCurrentUser";

        public const string Sys_User_Get = "Sys_User_Get";
        public const string WAS_Sys_User_Get = "WAS_Sys_User_Get";

        public const string Sys_User_Create = "Sys_User_Create";
        public const string WAS_Sys_User_Create = "WAS_Sys_User_Create";
        public const string Sys_User_Create_InvalidUserCode = "Err.DA.Sys_User_Create_InvalidUserCode";
        public const string Sys_User_Create_InvalidUserName = "Err.DA.Sys_User_Create_InvalidUserName";
        public const string Sys_User_Create_InvalidUserPassword = "Err.DA.Sys_User_Create_InvalidUserPassword";

        public const string Sys_User_Activate = "Sys_User_Activate";
        public const string Sys_User_Activate_InvalidPowerful = "Err.DA.Sys_User_Activate_InvalidPowerful";
        #endregion

        #region // Mst_Region
        public const string Mst_Region_CheckDB_RegionNotFound = "Err.DA.Mst_Region_CheckDB_AreaNotFound";
        public const string Mst_Region_CheckDB_RegionExist = "Err.DA.Mst_Region_CheckDB_AreaExist";
        public const string Mst_Region_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_Region_CheckDB_FlagActiveNotMatched";

        public const string Mst_Region_Get = "Mst_Region_Get";
        public const string WAS_Mst_Region_Get = "WAS_Mst_Region_Get";
        #endregion

        #region // Mst_TourType
        public const string Mst_TourType_CheckDB_TourTypeNotFound = "Err.DA.Mst_TourType_CheckDB_TourTypeNotFound";
        public const string Mst_TourType_CheckDB_TourTypeExist = "Err.DA.Mst_TourType_CheckDB_TourTypeExist";
        public const string Mst_TourType_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_TourType_CheckDB_FlagActiveNotMatched";

        public const string Mst_TourType_Get = "Mst_TourType_Get";
        public const string WAS_Mst_TourType_Get = "WAS_Mst_TourType_Get";
        #endregion

        #region // Mst_Tour
        public const string Mst_Tour_CheckDB_TourNotFound = "Err.DA.Mst_Tour_CheckDB_TourNotFound";
        public const string Mst_Tour_CheckDB_TourExist = "Err.DA.Mst_Tour_CheckDB_TourExist";
        public const string Mst_Tour_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_Tour_CheckDB_FlagActiveNotMatched";

        public const string Mst_Tour_Get = "Mst_Tour_Get";
        public const string WAS_Mst_Tour_Get = "WAS_Mst_Tour_Get";

        public const string Mst_Tour_Create = "Mst_Tour_Create";
        public const string WAS_Mst_Tour_Create = "WAS_Mst_Tour_Create";
        public const string Mst_Tour_CreateX_InvalidTourCode = "Err.DA.Mst_Tour_CreateX_InvalidTourCode";
        public const string Mst_Tour_CreateX_InvalidTourName = "Err.DA.Mst_Tour_CreateX_InvalidTourName";
        public const string Mst_Tour_CreateX_InvalidTouristNumber = "Err.DA.Mst_Tour_CreateX_InvalidTouristNumber";
        public const string Mst_Tour_CreateX_InvalidTourTotalPrice = "Err.DA.Mst_Tour_CreateX_InvalidTourTotalPrice";

        public const string Mst_Tour_Update = "Mst_Tour_Update";
        public const string WAS_Mst_Tour_Update = "WAS_Mst_Tour_Update";
        public const string Mst_Tour_Update_InvalidTourName = "Err.DA.Mst_Tour_Update_InvalidTourName";

        public const string Mst_Tour_Delete = "Mst_Tour_Delete";
        public const string WAS_Mst_Tour_Delete = "WAS_Mst_Tour_Delete";
        #endregion

        #region // Mst_CustomerType
        public const string Mst_CustomerType_CheckDB_CustomerTypeNotFound = "Err.DA.Mst_CustomerType_CheckDB_CustomerTypeNotFound";
        public const string Mst_CustomerType_CheckDB_CustomerTypeExist = "Err.DA.Mst_CustomerType_CheckDB_CustomerTypeExist";
        public const string Mst_CustomerType_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_CustomerType_CheckDB_FlagActiveNotMatched";

        public const string Mst_CustomerType_Get = "Mst_CustomerType_Get";
        public const string WAS_Mst_CustomerType_Get = "WAS_Mst_CustomerType_Get";
        #endregion

        #region // Mst_Customer
        public const string Mst_Customer_CheckDB_CustomerNotFound = "Err.DA.Mst_Customer_CheckDB_CustomerNotFound";
        public const string Mst_Customer_CheckDB_CustomerExist = "Err.DA.Mst_Customer_CheckDB_CustomerExist";
        public const string Mst_Customer_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_Customer_CheckDB_FlagActiveNotMatched";

        public const string Mst_Customer_Get = "Mst_Customer_Get";
        public const string WAS_Mst_Customer_Get = "WAS_Mst_Customer_Get";

        public const string Mst_Customer_Create = "Mst_Customer_Create";
        public const string WAS_Mst_Customer_Create = "WAS_Mst_Customer_Create";
        public const string Mst_Customer_CreateX_InvalidCustomerCode = "Err.DA.Mst_Customer_CreateX_InvalidCustomerCode";
        public const string Mst_Customer_CreateX_InvalidCustomerName = "Err.DA.Mst_Customer_CreateX_InvalidCustomerName";
        public const string Mst_Customer_CreateX_InvalidCustomerMobileNo = "Err.DA.Mst_Customer_CreateX_InvalidCustomerMobileNo";
        public const string Mst_Customer_CreateX_InvalidCustomerEmail = "Err.DA.Mst_Customer_CreateX_InvalidCustomerEmail";

        public const string Mst_Customer_Update = "Mst_Customer_Update";
        public const string WAS_Mst_Customer_Update = "WAS_Mst_Customer_Update";
        public const string Mst_Customer_UpdateX_InvalidCustomerMobileNo = "Err.DA.Mst_Customer_UpdateX_InvalidCustomerMobileNo";
        public const string Mst_Customer_UpdateX_InvalidCustomerEmail = "Err.DA.Mst_Customer_UpdateX_InvalidCustomerEmail";

        public const string Mst_Customer_Delete = "Mst_Customer_Delete";
        public const string WAS_Mst_Customer_Delete = "WAS_Mst_Customer_Delete";
        #endregion

        #region // Mst_TourDetail
        public const string Mst_TourDetail_CheckDB_TourDetailNotFound = "Err.DA.Mst_TourDetail_CheckDB_TourDetailNotFound";
        public const string Mst_TourDetail_CheckDB_TourDetailExist = "Err.DA.Mst_TourDetail_CheckDB_TourDetailExist";
        public const string Mst_TourDetail_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_TourDetail_CheckDB_FlagActiveNotMatched";

        public const string Mst_TourDetail_Get = "Mst_TourDetail_Get";
        public const string WAS_Mst_TourDetail_Get = "WAS_Mst_TourDetail_Get";

        public const string Mst_TourDetail_GetForView = "Mst_TourDetail_GetForView";
        public const string WAS_Mst_TourDetail_GetForView = "WAS_Mst_TourDetail_GetForView";
        public const string Mst_TourDetail_GetForViewAll = "Mst_TourDetail_GetForViewAll";
        public const string WAS_Mst_TourDetail_GetForViewAll = "WAS_Mst_TourDetail_GetForViewAll";

        public const string Mst_TourDetail_Create = "Mst_TourDetail_Create";
        public const string WAS_Mst_TourDetail_Create = "WAS_Mst_TourDetail_Create";

        public const string Mst_TourDetail_Update = "Mst_TourDetail_Update";
        public const string WAS_Mst_TourDetail_Update = "WAS_Mst_TourDetail_Update";

        public const string Mst_TourDetail_Delete = "Mst_TourDetail_Delete";
        public const string WAS_Mst_TourDetail_Delete = "WAS_Mst_TourDetail_Delete";
        #endregion

        #region // Mst_Province
        public const string Mst_Province_CheckDB_ProvinceNotFound = "Err.DA.Mst_Province_CheckDB_ProvinceNotFound";
        public const string Mst_Province_CheckDB_ProvinceExist = "Err.DA.Mst_Province_CheckDB_ProvinceExist";
        public const string Mst_Province_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_Province_CheckDB_FlagActiveNotMatched";

        public const string Mst_Province_Get = "Mst_Province_Get";
        public const string WAS_Mst_Province_Get = "WAS_Mst_Province_Get";
        #endregion

        #region // Mst_TourGuide
        public const string Mst_TourGuide_CheckDB_RegionNotFound = "Err.DA.Mst_TourGuide_CheckDB_RegionNotFound";
        public const string Mst_TourGuide_CheckDB_RegionExist = "Err.DA.Mst_TourGuide_CheckDB_RegionExist";
        public const string Mst_TourGuide_CheckDB_FlagActiveNotMatched = "Err.DA.Mst_TourGuide_CheckDB_FlagActiveNotMatched";

        public const string Mst_TourGuide_Get = "Mst_TourGuide_Get";
        public const string WAS_Mst_TourGuide_Get = "WAS_Mst_TourGuide_Get";

        public const string Mst_TourGuide_Create = "Mst_TourGuide_Create";
        public const string WAS_Mst_TourGuide_Create = "WAS_Mst_TourGuide_Create";
        public const string Mst_TourGuide_CreateX_InvalidTGNo = "Err.DA.Mst_TourGuide_CreateX_InvalidTGNo";
        public const string Mst_TourGuide_CreateX_InvalidTGName = "Err.DA.Mst_TourGuide_CreateX_InvalidTGName";
        public const string Mst_TourGuide_CreateX_InvalidTGAddress = "Err.DA.Mst_TourGuide_CreateX_InvalidTGAddress";
        public const string Mst_TourGuide_CreateX_InvalidTGMobileNo = "Err.DA.Mst_TourGuide_CreateX_InvalidTGMobileNo";

        public const string Mst_TourGuide_Update = "Mst_TourGuide_Update";
        public const string WAS_Mst_TourGuide_Update = "WAS_Mst_TourGuide_Update";
        public const string Mst_TourGuide_Update_InvalidTGName = "Err.DA.Mst_TourGuide_Update_InvalidTGName";
        public const string Mst_TourGuide_Update_InvalidTGAddress = "Err.DA.Mst_TourGuide_Update_InvalidTGAddress";
        public const string Mst_TourGuide_Update_InvalidTGMobileNo = "Err.DA.Mst_TourGuide_Update_InvalidTGMobileNo";

        public const string Mst_TourGuide_Delete = "Mst_TourGuide_Delete";
        public const string WAS_Mst_TourGuide_Delete = "WAS_Mst_TourGuide_Delete";
        #endregion

        #region // POW_NewsType
        public const string POW_NewsType_CheckDB_RegionNotFound = "Err.DA.POW_NewsType_CheckDB_RegionNotFound";
        public const string POW_NewsType_CheckDB_RegionExist = "Err.DA.POW_NewsType_CheckDB_RegionExist";
        public const string POW_NewsType_CheckDB_FlagActiveNotMatched = "Err.DA.POW_NewsType_CheckDB_FlagActiveNotMatched";

        public const string POW_NewsType_Get = "POW_NewsType_Get";
        public const string WAS_POW_NewsType_Get = "WAS_POW_NewsType_Get";
        #endregion

        #region // POW_NewsNews
        public const string POW_NewsNews_CheckDB_RegionNotFound = "Err.DA.POW_NewsNews_CheckDB_RegionNotFound";
        public const string POW_NewsNews_CheckDB_RegionExist = "Err.DA.POW_NewsNews_CheckDB_RegionExist";
        public const string POW_NewsNews_CheckDB_FlagActiveNotMatched = "Err.DA.POW_NewsNews_CheckDB_FlagActiveNotMatched";

        public const string POW_NewsNews_Get = "POW_NewsNews_Get";
        public const string WAS_POW_NewsNews_Get = "WAS_POW_NewsNews_Get";

        public const string POW_NewsNews_Create = "POW_NewsNews_Create";
        public const string WAS_POW_NewsNews_Create = "WAS_POW_NewsNews_Create";
        public const string POW_NewsNews_CreateX_InvalidNewsNo = "Err.DA.POW_NewsNews_CreateX_InvalidNewsNo";
        public const string POW_NewsNews_CreateX_InvalidTitle = "Err.DA.POW_NewsNews_CreateX_InvalidTitle";
        public const string POW_NewsNews_CreateX_InvalidThemeImage = "Err.DA.POW_NewsNews_CreateX_InvalidThemeImage";
        public const string POW_NewsNews_CreateX_InvalidContent = "Err.DA.POW_NewsNews_CreateX_InvalidContent";

        public const string POW_NewsNews_Update = "Err.DA.POW_NewsNews_Update";
        public const string WAS_POW_NewsNews_Update = "WAS_POW_NewsNews_Update";
        public const string POW_NewsNews_Update_InvalidThemeImage = "Err.DA.POW_NewsNews_Update_InvalidThemeImage";
        public const string POW_NewsNews_Update_InvalidTitle = "Err.DA.POW_NewsNews_Update_InvalidTitle";
        public const string POW_NewsNews_Update_InvalidContent = "Err.DA.POW_NewsNews_Update_InvalidContent";

        public const string POW_NewsNews_Delete = "POW_NewsNews_Delete";
        public const string WAS_POW_NewsNews_Delete = "WAS_POW_NewsNews_Delete";
        #endregion

        #region // POW_Recruitment
        public const string POW_Recruitment_CheckDB_RecNoNotFound = "Err.DA.POW_Recruitment_CheckDB_RecNoNotFound";
        public const string POW_Recruitment_CheckDB_RecNoExist = "Err.DA.POW_Recruitment_CheckDB_RecNoExist";
        public const string POW_Recruitment_CheckDB_FlagActiveNotMatched = "Err.DA.POW_Recruitment_CheckDB_FlagActiveNotMatched";

        public const string WAS_POW_Recruitment_Get = "WAS_POW_Recruitment_Get";
        public const string POW_Recruitment_Get = "POW_Recruitment_Get";

        public const string WAS_POW_Recruitment_Create = "WAS_POW_Recruitment_Create";
        public const string POW_Recruitment_Create = "POW_Recruitment_Create";
        public const string POW_Recruitment_CreateX_InvalidRecNo = "Err.DA.POW_Recruitment_CreateX_InvalidRecNo";
        public const string POW_Recruitment_CreateX_InvalidTitle = "Err.DA.POW_Recruitment_CreateX_InvalidTitle";
        public const string POW_Recruitment_CreateX_InvalidThemeImage = "Err.DA.POW_Recruitment_CreateX_InvalidThemeImage";
        public const string POW_Recruitment_CreateX_InvalidContent = "Err.DA.POW_Recruitment_CreateX_InvalidContent";

        public const string WAS_POW_Recruitment_Update = "WAS_POW_Recruitment_Update";
        public const string POW_Recruitment_Update = "POW_Recruitment_Update";
        public const string POW_Recruitment_Update_InvalidThemeImage = "Err.DA.POW_Recruitment_Update_InvalidThemeImage";
        public const string POW_Recruitment_Update_InvalidTitle = "Err.DA.POW_Recruitment_Update_InvalidTitle";
        public const string POW_Recruitment_Update_InvalidContent = "Err.DA.POW_Recruitment_Update_InvalidContent";

        public const string WAS_POW_Recruitment_Delete = "WAS_POW_Recruitment_Delete";
        public const string POW_Recruitment_Delete = "POW_Recruitment_Delete";
        #endregion

        #region // POW_AboutUs
        public const string POW_AboutUs_CheckDB_AUNoNotFound = "Err.DA.POW_AboutUs_CheckDB_AUNoNotFound";
        public const string POW_AboutUs_CheckDB_AUNoExist = "Err.DA.POW_AboutUs_CheckDB_AUNoExist";
        public const string POW_AboutUs_CheckDB_FlagActiveNotMatched = "Err.DA.POW_AboutUs_CheckDB_FlagActiveNotMatched";

        public const string POW_AboutUs_Get = "POW_AboutUs_Get";
        public const string WAS_POW_AboutUs_Get = "WAS_POW_AboutUs_Get";

        public const string WAS_POW_AboutUs_Create = "WAS_POW_AboutUs_Create";
        public const string POW_AboutUs_Create = "POW_AboutUs_Create";
        public const string POW_AboutUs_CreateX_InvalidAUNo = "Err.DA.POW_AboutUs_CreateX_InvalidAUNo";
        public const string POW_AboutUs_CreateX_InvalidTitle = "Err.DA.POW_AboutUs_CreateX_InvalidTitle";
        public const string POW_AboutUs_CreateX_InvalidVideoURL = "Err.DA.POW_AboutUs_CreateX_InvalidVideoURL";

        public const string WAS_POW_AboutUs_Update = "WAS_POW_AboutUs_Update";
        public const string POW_AboutUs_Update = "POW_AboutUs_Update";
        public const string POW_AboutUs_Update_InvalidVideoURL = "Err.DA.POW_AboutUs_Update_InvalidVideoURL";
        public const string POW_AboutUs_Update_InvalidTitle = "Err.DA.POW_AboutUs_Update_InvalidTitle";

        public const string WAS_POW_AboutUs_Delete = "WAS_POW_AboutUs_Delete";
        public const string POW_AboutUs_Delete = "POW_AboutUs_Delete";
        #endregion

        #region // POW_Contact
        public const string POW_Contact_CheckDB_ContactNoNotFound = "Err.DA.POW_Contact_CheckDB_ContactNoNotFound";
        public const string POW_Contact_CheckDB_ContactNoExist = "Err.DA.POW_Contact_CheckDB_ContactNoExist";
        public const string POW_Contact_CheckDB_FlagActiveNotMatched = "Err.DA.POW_Contact_CheckDB_FlagActiveNotMatched";

        public const string WAS_POW_Contact_Get = "WAS_POW_Contact_Get";
        public const string POW_Contact_Get = "POW_Contact_Get";

        public const string WAS_POW_Contact_Create = "WAS_POW_Contact_Create";
        public const string POW_Contact_Create = "POW_Contact_Create";
        public const string POW_Contact_CreateX_InvalidContactNo = "Err.DA.POW_Contact_CreateX_InvalidContactNo";
        public const string POW_Contact_CreateX_InvalidContactAddress = "Err.DA.POW_Contact_CreateX_InvalidContactAddress";
        public const string POW_Contact_CreateX_InvalidContactPhoneNo = "Err.DA.POW_Contact_CreateX_InvalidContactPhoneNo";

        public const string WAS_POW_Contact_Update = "WAS_POW_Contact_Update";
        public const string POW_Contact_Update = "POW_Contact_Update";
        public const string POW_Contact_Update_InvalidContactAddress = "Err.DA.POW_Contact_Update_InvalidContactAddress";
        public const string POW_Contact_Update_InvalidContactPhoneNo = "Err.DA.POW_Contact_Update_InvalidContactPhoneNo";

        public const string WAS_POW_Contact_Delete = "WAS_POW_Contact_Delete";
        public const string POW_Contact_Delete = "POW_Contact_Delete";
        #endregion

        #region // POW_ContactEmail
        public const string POW_ContactEmail_CheckDB_CENoNotFound = "Err.DA.POW_ContactEmail_CheckDB_CENoNotFound";
        public const string POW_ContactEmail_CheckDB_CENoExist = "Err.DA.POW_ContactEmail_CheckDB_CENoExist";
        public const string POW_ContactEmail_CheckDB_FlagActiveNotMatched = "Err.DA.POW_ContactEmail_CheckDB_FlagActiveNotMatched";

        public const string WAS_POW_ContactEmail_Get = "WAS_POW_ContactEmail_Get";
        public const string POW_ContactEmail_Get = "POW_ContactEmail_Get";

        public const string WAS_POW_ContactEmail_Create = "WAS_POW_ContactEmail_Create";
        public const string POW_ContactEmail_Create = "POW_ContactEmail_Create";
        public const string POW_ContactEmail_CreateX_InvalidCENo = "Err.DA.POW_ContactEmail_CreateX_InvalidCENo";

        public const string WAS_POW_ContactEmail_Delete = "WAS_POW_ContactEmail_Delete";
        public const string POW_ContactEmail_Delete = "POW_ContactEmail_Delete";
        #endregion

        #region // POW_FAQ:
        public const string POW_FAQ_CheckDB_FAQNoNotFound = "Err.DA.POW_FAQ_CheckDB_FAQNoNotFound";
        public const string POW_FAQ_CheckDB_FAQNoExist = "Err.DA.POW_FAQ_CheckDB_FAQNoExist";
        public const string POW_FAQ_CheckDB_FlagActiveNotMatched = "Err.DA.POW_FAQ_CheckDB_FlagActiveNotMatched";

        public const string WAS_POW_FAQ_Get = "WAS_POW_FAQ_Get";
        public const string POW_FAQ_Get = "POW_FAQ_Get";

        public const string WAS_POW_FAQ_Create = "WAS_POW_FAQ_Create";
        public const string POW_FAQ_Create = "POW_FAQ_Create";
        public const string POW_FAQ_CreateX_InvalidFAQNo = "Err.DA.POW_FAQ_CreateX_InvalidFAQNo";
        public const string POW_FAQ_CreateX_InvalidQuestion = "Err.DA.POW_FAQ_CreateX_InvalidQuestion";
        public const string POW_FAQ_CreateX_InvalidAnswer = "Err.DA.POW_FAQ_CreateX_InvalidAnswer";

        public const string WAS_POW_FAQ_Update = "WAS_POW_FAQ_Update";
        public const string POW_FAQ_Update = "POW_FAQ_Update";
        public const string POW_FAQ_Update_InvalidQuestion = "Err.DA.POW_FAQ_Update_InvalidQuestion";
        public const string POW_FAQ_Update_InvalidAnswer = "Err.DA.POW_FAQ_Update_InvalidAnswer";

        public const string WAS_POW_FAQ_Delete = "WAS_POW_FAQ_Delete";
        public const string POW_FAQ_Delete = "POW_FAQ_Delete";
        #endregion

        #region // Mst_Article:
        public const string Mst_Article_CheckDB_ArticleNoNotFound = "ErrDA.Mst_Article_CheckDB_ArticleNoNotFound";
        public const string Mst_Article_CheckDB_ArticleNoExist = "ErrDA.Mst_Article_CheckDB_ArticleNoExist";
        public const string Mst_Article_CheckDB_FlagActiveNotMatched = "ErrDA.Mst_Article_CheckDB_FlagActiveNotMatched";

        public const string Mst_Article_Get = "Mst_Article_Get";
        public const string WAS_Mst_Article_Get = "WAS_Mst_Article_Get";

        public const string Mst_Article_Create = "Mst_Article_Create";
        public const string WAS_Mst_Article_Create = "WAS_Mst_Article_Create";
        public const string Mst_Article_CreateX_Mst_ArticleDetailTblInvalid = "ErrDA.Mst_Article_CreateX_Mst_ArticleDetailTblInvalid";
        public const string Mst_Article_CreateX_Mst_ArticleDetailTblNotFound = "ErrDA.Mst_Article_CreateX_Mst_ArticleDetailTblNotFound";
        public const string Mst_Article_CreateX_InvalidArticleNo = "ErrDA.Mst_Article_CreateX_InvalidArticleNo";
        public const string Mst_Article_CreateX_InvalidArticleThemePath = "ErrDA.Mst_Article_CreateX_InvalidArticleThemePath";
        public const string Mst_Article_CreateX_InvalidArticleTitle = "ErrDA.Mst_Article_CreateX_InvalidArticleTitle";

        public const string Mst_Article_Update = "Mst_Article_Update";
        public const string WAS_Mst_Article_Update = "WAS_Mst_Article_Update";
        public const string Mst_Article_UpdateX_InvalidArticleTitle = "ErrDA.Mst_Article_UpdateX_InvalidArticleTitle";
        public const string Mst_Article_UpdateX_InvalidArticleThemePath = "ErrDA.Mst_Article_UpdateX_InvalidArticleThemePath";

        public const string Mst_Article_Delete = "Mst_Article_Delete";
        public const string WAS_Mst_Article_Delete = "WAS_Mst_Article_Delete";
        #endregion
    }
}
