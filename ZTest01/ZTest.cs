using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using System.Xml;
//using System.Xml.Linq;
using System.Threading;
//using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Routing;
////
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Channels;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
//using TDAL = EzDAL.MyDB;
//using TDALUtils = EzDAL.Utils;
using TConst = idn.Skycic.Inventory.Constants;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Globalization;
//using TUtils = IDM.Utils;
//using TError = IDM.Errors;

using idn.Skycic.Inventory.Common.Models;
using System.Security.Cryptography;
using TJson = Newtonsoft.Json;

// //
using ZTest01.ServiceReference1;
using ZTest01.WSNhanHSoThue;

// //
using inos.common.Model;
using OSiNOSSv = inos.common.Service;

using System.Security;
using idn.Skycic.Inventory.BizService.Services;

namespace ZTest01
{
	public partial class ZTest : Form
	{
		#region // Constructors and Destructors:
		public ZTest()
		{
			InitializeComponent();
		}

		#endregion

		#region // TestMix:
		private void btnTestMix_Click(object sender, EventArgs e)
		{
			try
			{
                //TestMix_HuongTa_SplitString();
                //TestMix_01_RSA();
                //TestMix_01_CallService();
                //TestMix_02_CallService();
                //TestMix_02_GetSimpleHash();
                //TestMix_02_VPBankLDAP();
                //TestMix_StringEqualIgnoreCase();
                //TestMix_02_GetJson();
                //TestMix_DateTimeUTC();
                //TestMix_DTimeRange();
                //TestMix_01_Std();
                //Test_XMLToDS();
                //Test_UTC();
                //TestMix_01_Base64();
                Test_Mix_CalliNOSDLL_01();
                //Test_Mix_Code36_01();
                //Test_Mix_CallWebAPI_01();
                //TestMix_01_String();
            }
			catch (Exception exc)
			{
				CommonForms.Utils.ProcessExc(exc);
			}
		}

		private void TestMix_01_Base64()
		{
			////
			bool bTest = false;
			int nSeq = 0;
			string strNetworkId = "3296932000";
			string strEncodeBase64 = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48REt5VGh1ZURUdSB4bWxucz0iaHR0cDovL2tla2hhaXRodWUuZ2R0Lmdvdi52bi9IU29ES3kiPjxES3lUaHVlIGlkPSJJRDEiPjxUVGluQ2h1bmc+PENRVD48bWFDUVQ+MTAxMDA8L21hQ1FUPjx0ZW5DUVQ+PC90ZW5DUVQ+PERWdT48bWFEVnU+MDAxOTwvbWFEVnU+PHRlbkRWdT5pZG9jTmV0PC90ZW5EVnU+PHNvR1BoZXBLRG9hbmg+PC9zb0dQaGVwS0RvYW5oPjwvRFZ1PjwvQ1FUPjxUVGluREt5VGh1ZT48bWFES3k+MjE2PC9tYURLeT48bWF1REt5PjAxLURLX1QtVmFuPC9tYXVES3k+PHRlbkRLeT7EkMSDbmcga8O9IHPhu60gZOG7pW5nIGThu4tjaCB24bulIFRWQU48L3RlbkRLeT48cEJhbkRLeT4yLjAuOTwvcEJhbkRLeT48bmdheURLeT4yMDE5LTA1LTIzPC9uZ2F5REt5Pjx0SU4+MDEwMDExMjE4NzwvdElOPjx0ZW5OTlQ+Q8OUTkcgVFkgQ+G7lCBQSOG6pk4gUVXhu5BDIFThur4gTE9ORyBRVUFORzwvdGVuTk5UPjwvVFRpbkRLeVRodWU+PC9UVGluQ2h1bmc+PE5EdW5nREt5PjxkaWFEaWVtVEI+PC9kaWFEaWVtVEI+PGlzc3Vlcj5ORVdURUwtQ0EgdjI8L2lzc3Vlcj48c3ViamVjdD5Dw5RORyBUWSBD4buUIFBI4bqmTiBRVeG7kEMgVOG6viBMT05HIFFVQU5HPC9zdWJqZWN0PjxzZXJpYWw+NTQwMTAxMDk5MkFGREJFQzNFNURFMzJCOUZDMENBQTE8L3NlcmlhbD48ZW1haWw+aHVvbmdudkBpZG9jbmV0LmNvbTwvZW1haWw+PHRlbD4wOTg2NTQ2Mzk5PC90ZWw+PGZyb21EYXRlQ0E+MjAxOS0wMS0xMSAxNjo0Mzo0OTwvZnJvbURhdGVDQT48dG9EYXRlQ0E+MjAyMC0wMS0xMSAxNjo0Mzo0OTwvdG9EYXRlQ0E+PGRreVREVD5mYWxzZTwvZGt5VERUPjxra1REVD50cnVlPC9ra1REVD48dGVuVG9DaHVjPk5FV1RFTC1DQSB2MjwvdGVuVG9DaHVjPjxzb0dpYXlDTmhhbj4xMDEwMDwvc29HaWF5Q05oYW4+PC9ORHVuZ0RLeT48L0RLeVRodWU+PENLeURUdT48U2lnbmF0dXJlIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIj48U2lnbmVkSW5mbz48Q2Fub25pY2FsaXphdGlvbk1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnL1RSLzIwMDEvUkVDLXhtbC1jMTRuLTIwMDEwMzE1IiAvPjxTaWduYXR1cmVNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjcnNhLXNoYTEiIC8+PFJlZmVyZW5jZSBVUkk9IiNJRDEiPjxUcmFuc2Zvcm1zPjxUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjZW52ZWxvcGVkLXNpZ25hdHVyZSIgLz48VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvVFIvMjAwMS9SRUMteG1sLWMxNG4tMjAwMTAzMTUiIC8+PC9UcmFuc2Zvcm1zPjxEaWdlc3RNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjc2hhMSIgLz48RGlnZXN0VmFsdWU+NVZIdzZjbjdMVVNBbUpWR2VjQTMzblhCQklzPTwvRGlnZXN0VmFsdWU+PC9SZWZlcmVuY2U+PC9TaWduZWRJbmZvPjxTaWduYXR1cmVWYWx1ZT5IcCt0RWVNT21DbzNVUFI2UEh1RHlUSFdkcmsvejd5V2Qzek9tblBvNGsvQjVuMWExSGZIK01tOHhBYml2Y01CNzhML1A0cFJDVjVic3VSaHJWdDhlNjlIbEpkLzUzYk0vNWx0NVJKTFA4VUZNV1gycXEwblorV0d6N2x1ZWJxRGdzNFpzSXgrMkoyZ0JVRUUzMVpjTVgyaW9nQXdFTnhkb0JsZjZneTl0N0k9PC9TaWduYXR1cmVWYWx1ZT48S2V5SW5mbz48S2V5VmFsdWU+PFJTQUtleVZhbHVlPjxNb2R1bHVzPjh0UzRxN01WdTN4bnhYSS9OTTM3aGM0NmNIcnRvRTlXckFNOWFMVFIyMVFCaWZFTmRYcXNvVXZzTVQzdnpyRUpNMURRQjRZckprVWREUXF0OGVoVW00MGFaQjBDNFRZOVlFMGZ0RENSV2RSUVYxUytHYWQrdDY5REpZc0dVOFdjM296ejg5Rmwrc21BN0pnODFyOG1lRDd5c3FhbnVOcktHWkZML3liSjJKaz08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+PC9LZXlWYWx1ZT48WDUwOURhdGE+PFg1MDlTdWJqZWN0TmFtZT5DPVZOLCBDTj1Dw5RORyBUWSBD4buUIFBI4bqmTiBRVeG7kEMgVOG6viBMT05HIFFVQU5HLCBPSUQuMC45LjIzNDIuMTkyMDAzMDAuMTAwLjEuMT1NU1Q6MDEwNzYwMDkwOTwvWDUwOVN1YmplY3ROYW1lPjxYNTA5Q2VydGlmaWNhdGU+TUlJRWlEQ0NBM0NnQXdJQkFnSVFWQUVCQ1pLdjIrdytYZU1ybjhES29UQU5CZ2txaGtpRzl3MEJBUVVGQURCZ01Rc3dDUVlEVlFRR0V3SldUakU2TURnR0ExVUVDZ3d4UThPVVRrY2dWRmtnUStHN2xDQlFTT0c2cGs0Z1Zrbmh1NFJPSUZSSXc1Uk9SeUJPUlZkVVJVd3RWRVZNUlVOUFRURVZNQk1HQTFVRUF4TU1Ua1ZYVkVWTUxVTkJJSFl5TUI0WERURTVNREV4TVRBNU5ETTBPVm9YRFRJd01ERXhNVEE1TkRNME9Wb3dZekVlTUJ3R0NnbVNKb21UOGl4a0FRRU1EazFUVkRvd01UQTNOakF3T1RBNU1UUXdNZ1lEVlFRRERDdER3NVJPUnlCVVdTQkQ0YnVVSUZCSTRicW1UaUJSVmVHN2tFTWdWT0c2dmlCTVQwNUhJRkZWUVU1SE1Rc3dDUVlEVlFRR0V3SldUakNCbnpBTkJna3Foa2lHOXcwQkFRRUZBQU9CalFBd2dZa0NnWUVBOHRTNHE3TVZ1M3hueFhJL05NMzdoYzQ2Y0hydG9FOVdyQU05YUxUUjIxUUJpZkVOZFhxc29VdnNNVDN2enJFSk0xRFFCNFlySmtVZERRcXQ4ZWhVbTQwYVpCMEM0VFk5WUUwZnREQ1JXZFJRVjFTK0dhZCt0NjlESllzR1U4V2Mzb3p6ODlGbCtzbUE3Smc4MXI4bWVEN3lzcWFudU5yS0daRkwveWJKMkprQ0F3RUFBYU9DQWIwd2dnRzVNQXdHQTFVZEV3RUIvd1FDTUFBd0h3WURWUjBqQkJnd0ZvQVVndkR4SWUvK1RsZzBjVy81akVJUFB5SGRWWnN3YXdZSUt3WUJCUVVIQVFFRVh6QmRNQzRHQ0NzR0FRVUZCekFDaGlKb2RIUndPaTh2Y0hWaU1pNXVaWGRqWVM1MmJpOXVaWGQwWld3dFkyRXVZM0owTUNzR0NDc0dBUVVGQnpBQmhoOW9kSFJ3T2k4dmIyTnpjREl1Ym1WM1kyRXVkbTR2Y21WemNHOXVaR1Z5TUNRR0ExVWRFUVFkTUJ1QkdXdHBibWhrYjJGdWFFQmpiMjVuZEhsaGJtZHBZUzVqYjIwd1d3WURWUjBnQkZRd1VqQlFCZ3dyQmdFRUFZSHRBd0VKQXdFd1FEQVpCZ2dyQmdFRkJRY0NBakFOREF0UFUxOVNaVzVsZDE4eFdUQWpCZ2dyQmdFRkJRY0NBUllYYUhSMGNEb3ZMM0IxWWk1dVpYZGpZUzUyYmk5eWNHRXdOQVlEVlIwbEJDMHdLd1lJS3dZQkJRVUhBd0lHQ0NzR0FRVUZCd01FQmdvckJnRUVBWUkzQ2dNTUJna3Foa2lHOXk4QkFRVXdNd1lEVlIwZkJDd3dLakFvb0NhZ0pJWWlhSFIwY0RvdkwyTnliREl1Ym1WM1kyRXVkbTR2Ym1WM2RHVnNMV05oTG1OeWJEQWRCZ05WSFE0RUZnUVVIMUl5TVlIaGFqbFlQOUJGdXRqRVpLVGpjYmt3RGdZRFZSMFBBUUgvQkFRREFnVHdNQTBHQ1NxR1NJYjNEUUVCQlFVQUE0SUJBUUFyWW92OEhnSmh6cTZFTVlOelNWVUpNN1A4aC9qOGF6R2ltU0ZpM2VubVVZMm8wVGVudWtGV0crenFtSU5GZ05TVHNKNFM1SGV0cjhrY3J4cGtZclF2a1JleVZ2VThuaUtwanFzdm5aWXBzNHlNdm9kRjV3NmxvOFhvVEJ3ajQvbTY5QmZwd2NSLzUwOHRpU1AzZUtRY0FGU1RwTUhsYW1NMXo2TGw5S2NVaVVORFdPc2E5OGhGVi83cmxBM2xKOWswaTNTMEY1N1FKbFdob3J0NE5GZGdzVTkrenk2Tnk3U1FYY3ZTNXZ0c3hkMThIaXNaZE9RbG43b0htNHorWFlIaWVjQmQwUm95MU8xZ2pJS1phYkF2dXZvYkd2aDV1QlZ5THQxdm5PYTloSWw3M21Qbi9RL3NoaG1LU05IM3VrNVg2TWNsRFBKNmcxT3dWTjVOK1YrYjwvWDUwOUNlcnRpZmljYXRlPjwvWDUwOURhdGE+PC9LZXlJbmZvPjwvU2lnbmF0dXJlPjxTaWduYXR1cmUgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyMiPjxTaWduZWRJbmZvPjxDYW5vbmljYWxpemF0aW9uTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvVFIvMjAwMS9SRUMteG1sLWMxNG4tMjAwMTAzMTUiIC8+PFNpZ25hdHVyZU1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyNyc2Etc2hhMSIgLz48UmVmZXJlbmNlIFVSST0iI0lEMSI+PFRyYW5zZm9ybXM+PFRyYW5zZm9ybSBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyNlbnZlbG9wZWQtc2lnbmF0dXJlIiAvPjwvVHJhbnNmb3Jtcz48RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnI3NoYTEiIC8+PERpZ2VzdFZhbHVlPjVWSHc2Y243TFVTQW1KVkdlY0EzM25YQkJJcz08L0RpZ2VzdFZhbHVlPjwvUmVmZXJlbmNlPjwvU2lnbmVkSW5mbz48U2lnbmF0dXJlVmFsdWU+SjR6NFYwcHFYWmg2UVJudDFJT2R2V210YU45cU54TWhGZTVKTUdtRFdDcVhBTnNueU5XNm5HK0h1a1JTUVNkZ3UwVDVPRzllUlNyVm1xcEoydkFmeVJmL1BodlBNbmc2RmxuaW9iRHVkbk9YaVp0TDRQOXdvb0NzbDIrdzFtczQzcklrYm5FYmMwZXdabllDOVlJMVJTMkdYYldWcEVOM2VLVmNCdnY5NzlZPTwvU2lnbmF0dXJlVmFsdWU+PEtleUluZm8+PEtleVZhbHVlPjxSU0FLZXlWYWx1ZT48TW9kdWx1cz5rNFNsQjhFZzhCTThkbUxvUHBqYngySnIwYkYrMWpJUTNlTE1oZURXQmJidWdibGxyQldway9qRGNhMFkxaHUxVWtmbWVKclozdjhJa3hXcGVGU2NLUTBsSzRleWhuTjhoZ1p1WnMybGRVeTg2WWMwcFhQZERoZ09BTGtHTzlObE05N2JFVXJTRWdNbHByWUNSSmdTTVR4d2RlMnRQWDMwQTNpSVFyYXA3aUU9PC9Nb2R1bHVzPjxFeHBvbmVudD5BUUFCPC9FeHBvbmVudD48L1JTQUtleVZhbHVlPjwvS2V5VmFsdWU+PFg1MDlEYXRhPjxYNTA5U3ViamVjdE5hbWU+Qz1WTiwgTD1Iw4AgTuG7mEksIENOPUPDlE5HIFRZIEPhu5QgUEjhuqZOIMSQ4bqmVSBUxq8gVsOAIEPDlE5HIE5HSOG7hiBJRE9DTkVUIChURVNUKSwgT0lELjAuOS4yMzQyLjE5MjAwMzAwLjEwMC4xLjE9TVNUOjAxMDQ2MTQ2OTI8L1g1MDlTdWJqZWN0TmFtZT48WDUwOUNlcnRpZmljYXRlPk1JSUQ0VENDQXNtZ0F3SUJBZ0lRVkFULy9yY0RQN01XMW5JZ0czMmk2ekFOQmdrcWhraUc5dzBCQVFVRkFEQTZNUXN3Q1FZRFZRUUdFd0pXVGpFV01CUUdBMVVFQ2hNTlZtbGxkSFJsYkNCSGNtOTFjREVUTUJFR0ExVUVBeE1LVm1sbGRIUmxiQzFEUVRBZUZ3MHhPVEEwTWpBd05qUTJOVEZhRncweE9UQTNNakV3TmpRMk5URmFNSUdMTVI0d0hBWUtDWkltaVpQeUxHUUJBUXdPVFZOVU9qQXhNRFEyTVRRMk9USXhTREJHQmdOVkJBTU1QMFBEbEU1SElGUlpJRVBodTVRZ1VFamh1cVpPSU1TUTRicW1WU0JVeHE4Z1ZzT0FJRVBEbEU1SElFNUhTT0c3aGlCSlJFOURUa1ZVSUNoVVJWTlVLVEVTTUJBR0ExVUVCd3dKU01PQUlFN2h1NWhKTVFzd0NRWURWUVFHRXdKV1RqQ0JuekFOQmdrcWhraUc5dzBCQVFFRkFBT0JqUUF3Z1lrQ2dZRUFrNFNsQjhFZzhCTThkbUxvUHBqYngySnIwYkYrMWpJUTNlTE1oZURXQmJidWdibGxyQldway9qRGNhMFkxaHUxVWtmbWVKclozdjhJa3hXcGVGU2NLUTBsSzRleWhuTjhoZ1p1WnMybGRVeTg2WWMwcFhQZERoZ09BTGtHTzlObE05N2JFVXJTRWdNbHByWUNSSmdTTVR4d2RlMnRQWDMwQTNpSVFyYXA3aUVDQXdFQUFhT0NBUk13Z2dFUE1EVUdDQ3NHQVFVRkJ3RUJCQ2t3SnpBbEJnZ3JCZ0VGQlFjd0FZWVphSFIwY0RvdkwyOWpjM0F1ZG1sbGRIUmxiQzFqWVM1MmJqQWRCZ05WSFE0RUZnUVVKQ3ZyNmF2NXZMSXAyRUV1UklBSFAzazk1bmd3REFZRFZSMFRBUUgvQkFJd0FEQWZCZ05WSFNNRUdEQVdnQlFJWU9ZZkd4VFpTSUFMWHFkY2N5VXNJQTM1MWpCNEJnTlZIUjhFY1RCdk1HMmdLNkFwaGlkb2RIUndPaTh2WTNKc0xuWnBaWFIwWld3dFkyRXVkbTR2Vm1sbGRIUmxiQzFEUVM1amNteWlQcVE4TURveEV6QVJCZ05WQkFNTUNsWnBaWFIwWld3dFEwRXhGakFVQmdOVkJBb01EVlpwWlhSMFpXd2dSM0p2ZFhBeEN6QUpCZ05WQkFZVEFsWk9NQTRHQTFVZER3RUIvd1FFQXdJRjREQU5CZ2txaGtpRzl3MEJBUVVGQUFPQ0FRRUFhQVdNS3Z5c2N5Z094M2Y2NVl3MDNhWFhrWS9qZVc3cklIRFMrSnVLbXM2amlseHN0bDA5Rm40TFIyMlI4NmNFcVdTcHBLN3lTVHhtVmNjM3JJVmY3di9IRmwyeS8rMkNHcEpEaE9xbWdDeGs1YVNRdXovNXUwaGwyNUc1YmNzdWdpRzBTK1c5ZFE0dEdHNjIrVW9iNmdQYzFlVnpPSzRPNmZPTlhIWU1iVllSMForWGtDa2tid2p4bDJXYm5tdHZlY21qRFNRVlJxUnMrdmtRWjlWZ0pXNmNyYW1XN2EvTndyR3lYR0plbm9TY0szT1V2c2hMVHNiTk5JbWw1UW9hMWswYUZoQ0NWV3RpZ1M3WjAzbEtOYTJYZGVtSS9NM05UQmoybVNDNmxjejd1OUhkYmtXVlZTWWNFcFF1MHNmbDVQUGNNcWNoUi8vTlQ0YVV5Z3RPQ3c9PTwvWDUwOUNlcnRpZmljYXRlPjwvWDUwOURhdGE+PC9LZXlJbmZvPjwvU2lnbmF0dXJlPjwvQ0t5RFR1PjwvREt5VGh1ZURUdT4=";
			string strTVAN_FilePath = @"d:\AllWebData\WebSites\idocNet.Test.HDDT.V22.3296932000.WA\api\File\";
			string strXMLFilePathBase = @"UploadedFiles\XMLFiles\";
			string strXMFilePath = strTVAN_FilePath + strXMLFilePathBase;
			

			string strTid = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nSeq++);

			if (!bTest)
			{
				string strDeCodeBase64 = TUtils.CUtils.Base64_Decode(strEncodeBase64);
				string strXMLFilePathSave = string.Format("{0}{1}.xml", strXMFilePath, (strTid + "." + strNetworkId));

				File.WriteAllText(strXMLFilePathSave, strDeCodeBase64);
			}
		}

		private void TestMix_01_CallService()
		{
			////
			bool bTest = false;
			string strUserPwd = "123456";
			string strUserPwdHash = "";

			#region // Get Encode Hash:
			if (bTest)
			{
				///
				strUserPwd = "RS.SA.bg.ALL.001";
				strUserPwdHash = TUtils.CUtils.GetEncodedHash(strUserPwd); // 2oT0soSe3cs8PKuqzN/m7w

				///
				strUserPwd = "MS.SA.bg.ALL.001";
				strUserPwdHash = TUtils.CUtils.GetEncodedHash(strUserPwd); // JwFK7d+n07Wey/Wsp7Rsvg
			}
			#endregion

			#region // Enum To List:
			if (bTest)
			{
				var dict = new Dictionary<int, string>();
				foreach (var name in Enum.GetNames(typeof(OrgSizes)))
				{
					dict.Add((int)Enum.Parse(typeof(OrgSizes), name), name);
				}

				///OrgSizes[] result = (OrgSizes[])Enum.GetValues(typeof(OrgSizes));

				string[] strResult = Enum.GetNames(typeof(OrgSizes));

				string strParse = (string)Enum.Parse(typeof(OrgSizes), strResult[0]);

				Array objArray = Enum.GetValues(typeof(OrgSizes));

				var listOfEnums = Enum.GetValues(typeof(OrgSizes)).Cast<OrgSizes>().ToList();

				Org objOrg = new Org();
				//object strOrgSize = result[0].ToString();

				//objOrg.OrgSize = (OrgSizes)strOrgSize;

				List<OrgSizes> list = Enum.GetValues(typeof(OrgSizes)).Cast<OrgSizes>().ToList();

				//List<NameValue> list = EnumToList<OrgSizes>();
			}
			#endregion

			#region // TimeSpan:
			if (bTest)
			{
				string strDtimeFirst = "2019-07-30 12:00:00";
				string strDtimeSecond = "2019-07-30 12:31:59";

				DateTime dateTimeFirst = Convert.ToDateTime(strDtimeFirst);
				DateTime dateTimeSecond = Convert.ToDateTime(strDtimeSecond);

				Int32 nMinutes = (dateTimeSecond - dateTimeFirst).Minutes;
			}
			#endregion

			#region // UserPwd Encrypt:
			if (bTest)
			{
				string strUserPwdEncrypt = TUtils.CUtils.Base64_Encode(strUserPwd);
				string strUserPwdDeEncrypt = TUtils.CUtils.Base64_Decode(strUserPwdEncrypt);
			}
			#endregion
		}

		private void Test_Mix_CalliNOSDLL_01()
		{
			bool bTest = false;
            //OrgSizes orgSizes = new OrgSizes();
            string strInosBaseUrl = "https://idn.inos.vn";
            inos.common.Paths.SetInosServerBaseAddress(strInosBaseUrl);

            OSiNOSSv.OrderService objOrderService = new OSiNOSSv.OrderService(null);
			OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);
			OSiNOSSv.OrgService objOrgService = new OSiNOSSv.OrgService(null);
			OSiNOSSv.LicService objLicService = new OSiNOSSv.LicService(null);
			OSiNOSSv.InosClientServiceBase objInosClientServiceBase = new OSiNOSSv.InosClientServiceBase(null);
            OSiNOSSv.NotificationService objNotifyServices = new OSiNOSSv.NotificationService(null);

            #region // Check User Exist:
            if (bTest)
			{
				InosUser dummy = new InosUser();
				dummy.Email = "QUYNHQUYNHABC1234@GMAIL.COM";

				InosUser inosUser = objAccountService.GetUser(dummy);
			}
			#endregion

			#region // LicService.GetAllPackages: NG.
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				string solutionCode = "HDDT";
				//long orgId = 3213284000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objLicService.GetAllPackages(
					solutionCode
					);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // OrgService.GetAllBizType:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//string solutionCode = "HDDT";
				//long orgId = 3213284000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				//var ret = objOrgService.();

				//object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // OrgService.GetAllBizType:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//string solutionCode = "HDDT";
				//long orgId = 3213284000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objOrgService.GetAllBizType();

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // OrgService.GetAllBizField:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//string solutionCode = "HDDT";
				//long orgId = 3213284000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objOrgService.GetAllBizField();

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			//objAccountService.AccessToken = "qgIquFrXDAHWjfAxVIm6JE3TbSEAN7MDuixCVFBTfjn_4mDyXrbH3u7B6h4wCP8RdRGgzzgaoEy9WBAkHU1fYq5vtKfLO_vrYKhst7BsUt-H7NMfwkqBc2KIFNDIBySoV6igOkhs52lqsXwPWLgu7tgV0lWt8aHqcNHWjG6dfdBN9HBO68tvifVGKwQgl5pWAn-cZQT8EL6MMz2bycWLb3QimtNrZToEP2kLbM2nZv3Pva0TlyrJuSM4nmQ91jBBni_B1A";
			var state = objAccountService.RequestToken("qinvoiceordadmin@inos.vn", "Csc123456", new string[] { "test" });
			//var state = objAccountService.RequestToken("DUNGVA@IDOCNET.COM", "123456", new string[] { "test" });
			//var user = objAccountService.GetCurrentUser();
			//var state = objAccountService.RequestToken("demo@idocnet.com", TConst.BizMix.Default_Password, new string[] { "test" });
			var user = objAccountService.GetCurrentUser();
			objOrgService.AccessToken = objAccountService.AccessToken;
			objLicService.AccessToken = objAccountService.AccessToken;
            objNotifyServices.AccessToken = objAccountService.AccessToken;
            #region // Notify New:
            if (!bTest)
            {
                var stateAccesstoken = objAccountService.RequestToken("demo@inos.vn", "123456", new string[] { "test" });

                objNotifyServices.AccessToken = objAccountService.AccessToken;

                InosUser objInosUser = new InosUser()
                {
                    Email = "demo@inos.vn".ToUpper()
                };

                //objNotifyServices.CreateNotifications
                InosNotification objInosNotify = new InosNotification()
                {
                    Id = 0,
                    NetworkId = 4221896000,
                    SolutionCode = "INVENTORY",
                    TypeCode = "QRbox.StockIn",
                    SubType = null,
                    UserId = 0,
                    Detail = "Đây là thông báo có FireBaseStatus: " + NotificationPartyStatuses.Pending,
                    //SendUserId = 0,
                    //Params = null,
                    Status = 0,
                    FirebaseStatus = NotificationPartyStatuses.Pending,
                    InosUser = objInosUser
                };

                List<InosNotification> lst_Notify = new List<InosNotification>();
                lst_Notify.Add(objInosNotify);

                var res = objNotifyServices.CreateNotifications(lst_Notify);

                //string jSon = TJson.JsonConvert.SerializeObject(lst_Notify);

                
                var objInosNotificationResult = objNotifyServices.GetNotifications(4221896000, "INVENTORY", inos.common.Model.NotificationStatuses.ALL, 0, 10);
                //string jSon1 = TJson.JsonConvert.SerializeObject(objInosNotificationResult);
            }
            #endregion

            if (bTest)
            {
                string Url = "https://test.skycic.com/";
                string strQInvoiceOrderAdmin_UserCode = "qinvoiceordadmin@inos.vn";
                string strQInvoiceOrderAdmin_UserPassword = "Csc123456";
                var model = new
                {
                    List = new List<dynamic>
                    {

                    }

                };

                string strMessage = CmUtils.StringUtils.Replace(
                            @"TestX"
                        //, "<InvF_In>", strInvCodeIn
                        //, "<InvF_Out>", strInvCodeOut
                        //, "<Time>", (dtimeSys.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss")
                        ).Trim();

                var objUserCreate = new GeneneralNotification()
                {
                    Email = "demo@inos.vn",
                    Detail = strMessage.Trim()
                };

                model.List.Add(objUserCreate);
                var strAuthorization = objAccountService.RequestToken(strQInvoiceOrderAdmin_UserCode, strQInvoiceOrderAdmin_UserPassword, new string[] { "test" }).AccessToken;
                GeneneralNotificationBatch objGeneneralNotificationBatch = new GeneneralNotificationBatch();
                
                objGeneneralNotificationBatch = OS_SkyCICService.Instance.WA_SkyCIC_NotificationApi_SendGeneralNotification(Url, strAuthorization, model);
            }

			#region // DiscountCode:
			if (bTest)
			{
				DiscountCode objDiscountCode = new DiscountCode();
				objDiscountCode.DiscountType = DiscountCodeTypes.Absolute;

				Inos_DiscountCode objInos_DiscountCode = new Inos_DiscountCode();
				objInos_DiscountCode.DiscountType = (Inos_DiscountCodeTypes)objDiscountCode.DiscountType;
			}
			#endregion

			#region // OrgService_CreateOrg:
			if (bTest)
			{
				object objOrgSize = 2;

				////
				Org objOrg = new Org();
				//objOrg.Id = 123; // Tạo Mới Chưa có.
				objOrg.ParentId = 0;
				objOrg.Name = "1112226666";
				objOrg.ShortName = "1112226666";

				////
				objOrg.BizType = new BizType();
				objOrg.BizType.Id = 1;
				objOrg.BizType.Name = "Default";

				////
				objOrg.BizField = new BizField();
				objOrg.BizField.Id = 1;
				objOrg.BizField.Name = "Default";

				////
				objOrg.OrgSize = new OrgSizes();
				objOrg.OrgSize = (OrgSizes)objOrgSize;
				objOrg.ContactName = "LONGQUANG";
				objOrg.Email = "QUYNHQUYNHABC1234@GMAIL.COM";
				objOrg.PhoneNo = "0986546399";
				objOrg.Description = "MyTest";
				objOrg.Enable = true;

				////
				objOrg.UserList = new List<OrgUser>();

				OrgUser objOrgUser = new OrgUser();
				objOrgUser.UserId = user.Id;
				objOrgUser.Name = "QUYNHQUYNHABC1234@GMAIL.COM";
				objOrgUser.Email = "QUYNHQUYNHABC1234@GMAIL.COM";
				objOrgUser.Status = OrgUserStatuses.Active;
				objOrgUser.Role = OrgUserRoles.Admin;

				objOrg.UserList.Add(objOrgUser);
				////
				objOrg.InviteList = null;

				////
				objOrg.CurrentUserRole = OrgUserRoles.Admin;

				////
				object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objOrgService.CreateOrg(objOrg);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion


			#region // objOrgService.DeleteInvite:
			if (bTest)
			{
				////

				//string strsolutionCode = "HDDT";
				//long orgId = 3296932000;
				//string solutionCode = "HDDT";
				//long orgId = 3296932000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);

				OrgInvite objOrgInvite = new OrgInvite();
				objOrgInvite.Email = "XUANND@IDOCNET.COM";
				objOrgInvite.OrgId = 3296932000;


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objOrgService.DeleteInvite(objOrgInvite);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // GetOrgSolutionModules:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				string solutionCode = "HDDT";
				long orgId = 3296932000;
				//long packageId = 3296932000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				List<LicModule> ret = objLicService.GetOrgSolutionModules(
					solutionCode
					, orgId
					);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // LicService_GetCurrentUserLicense:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				string solutionCode = "HDDT";
				long orgId = 3296932000;
				//long packageId = 3296932000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				UserLicense ret = objLicService.GetCurrentUserLicense(
					solutionCode
					, orgId
					);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			//UserLicense objUserLicense = objLicService.GetCurrentUserLicense("HDDT", 3296932000);

			#region // OrgService.GetOrgLicense:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//string solutionCode = "HDDT";
				long orgId = 3296932000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objLicService.GetOrgLicense(orgId);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // EditProfile User:
			if (bTest)
			{
				InosEditProfileModel dummy = new InosEditProfileModel();
				//dum
				dummy.OldPassword = "123";
				dummy.NewPassword = "123456";
				dummy.ChangePassword = true;

				InosUser inosUser = objAccountService.EditProfile(dummy);
			}
			#endregion

			#region // AccountService_Activate:
			if (bTest)
			{
				//OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);
				//string strid = "ec309a91c41f496caf938025cbc7201b";
				string strid = "a7c067d3bbb949e38c4b8e97ce808945";
				var ret = objAccountService.Activate(strid);
			}
			#endregion

			#region // OrgService.AddInvite:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//string solutionCode = "HDDT";
				//long orgId = 3213284000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);
				OrgInvite orgInvite = new OrgInvite();
				orgInvite.Email = "khanhvan0792@gmail.com";
				orgInvite.OrgId = 3295351000;


				var ret = objOrgService.AddInvite(orgInvite);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // OrgService.GetMyOrgList:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//string solutionCode = "HDDT";
				//long orgId = 3213284000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objOrgService.GetMyOrgList();

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // OrgService_CreateOrg:
			if (bTest)
			{
				object objOrgSize = 2;

				////
				Org objOrg = new Org();
				//objOrg.Id = 123; // Tạo Mới Chưa có.
				objOrg.ParentId = 3232411000;
				objOrg.Name = "5801407597";

				////
				objOrg.BizType = new BizType();
				objOrg.BizType.Id = 1;
				objOrg.BizType.Name = "Default";

				////
				objOrg.BizField = new BizField();
				objOrg.BizField.Id = 1;
				objOrg.BizField.Name = "Default";

				////
				objOrg.OrgSize = new OrgSizes();
				objOrg.OrgSize = (OrgSizes)objOrgSize;
				objOrg.ContactName = "LONGQUANG";
				objOrg.Email = "dongnt@idocnet.com";
				objOrg.PhoneNo = "0986546399";
				objOrg.Description = "MyTest";
				objOrg.Enable = true;

				////
				objOrg.UserList = new List<OrgUser>();

				OrgUser objOrgUser = new OrgUser();
				objOrgUser.UserId = 3193859000;
				objOrgUser.Name = "IDNDEMO";
				objOrgUser.Email = "demo@idocnet.com";
				objOrgUser.Status = OrgUserStatuses.Active;
				objOrgUser.Role = OrgUserRoles.Admin;

				objOrg.UserList.Add(objOrgUser);
				////
				objOrg.InviteList = null;

				////
				objOrg.CurrentUserRole = OrgUserRoles.Admin;

				////
				object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objOrgService.CreateOrg(objOrg);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // LicService.GetOrgSolution:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//string solutionCode = "HDDT";
				//long orgId = 3213284000;
				//long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				//var ret = objLicService.GetOrgSolutions(
				//	orgId
					
				//	);

				//object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // LicService_GetCurrentUserLicense:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				string solutionCode = "HDDT";
				//long orgId = 3213284000;
				long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				//object objJsonRQ = TJson.JsonConvert.SerializeObject(objOrg);

				var ret = objLicService.GetCurrentUserLicense(
					solutionCode
					, packageId
					);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // LicService_GetSolutionInPackages:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				long orgId = 3213284000;
				long packageId = 3166064000;
				//List<long> packageIds = new List<long>();
				//packageIds.Add(3166064000);


				////
				var ret = objLicService.GetSolutionInPackages(
					orgId
					, packageId
					);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // LicService_GetOrgSolution:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				//long orgId = 3213284000;
				List<long> packageIds = new List<long>();
				packageIds.Add(3166064000);


				////
				//var ret = objLicService.GetOrgSolution(
				//	orgId
				//	);

				//object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // LicService_RegisterPackages:
			if (bTest)
			{
				////
				//string strsolutionCode = "HDDT";
				//long orgId = 3194612000;
				long orgId = 3213284000;
				List<long> packageIds = new List<long>();
				packageIds.Add(3166064000);


				////
				var ret = objLicService.RegisterPackages(
					orgId
					, packageIds
					);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // AccountService_Register:
			if (bTest)
			{
				var userInfo = objAccountService.GetCurrentUser();
			}
			#endregion

			//#region // AccountService_Register:
			//if (bTest)
			//{
			//	var userInfo = objLicService.GetOrgLicense();
			//}
			//#endregion

			#region // AccountService_Register:
			if (bTest)
			{
				InosCreateUserModel objInosCreateUserModel = new InosCreateUserModel();

				objInosCreateUserModel.Email = "demo@idocnet.com";
				objInosCreateUserModel.Name = "IDNDEMO";
				objInosCreateUserModel.Password = TConst.InosMix.Default_Password;

				objInosCreateUserModel.Language = "vn";
				objInosCreateUserModel.TimeZone = 7;

				//OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);

				var ret = objAccountService.Register((InosCreateUserModel)objInosCreateUserModel);
			}
			#endregion

			#region // LicService_GetAllPackages:
			if (bTest)
			{
				////
				string strsolutionCode = "HDDT";

				////
				var ret = objLicService.GetAllPackages(strsolutionCode);

				object objJsonRT = TJson.JsonConvert.SerializeObject(ret);
			}
			#endregion

			#region // OrgService_GetAllBizType:
			if (bTest)
			{
				var ret = objOrgService.GetAllBizType();
			}
			#endregion

			#region // OrgService_GetAllBizField:
			if (bTest)
			{
				var ret = objOrgService.GetAllBizField();
			}
			#endregion

			#region // AccountService_Activate:
			if (bTest)
			{
				//OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);
				string strid = "ec309a91c41f496caf938025cbc7201b";
				var ret = objAccountService.Activate(strid);
			}
			#endregion

			#region // XML to DS:
			if (bTest)
			{
				//OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);
				//objAccountService.AccessToken = "";

				//var state = objAccountService.RequestToken("tuyenba@idocnet.com", "123456", new string[] { "test" });
				//var user = objAccountService.GetCurrentUser();

				InosCreateUserModel objInosCreateUserModel = new InosCreateUserModel();

				objInosCreateUserModel.Email = "dungnd@idocnet.com";
				objInosCreateUserModel.Name = "DUNGND";
				objInosCreateUserModel.Password = TConst.InosMix.Default_Password;

				objInosCreateUserModel.Language = "vn";
				objInosCreateUserModel.TimeZone = 7;

				//OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);

				var ret = objAccountService.Register((InosCreateUserModel)objInosCreateUserModel);
			}
			#endregion

			#region // Split String:
			if (bTest)
			{
				string strXML = @"--uuid:901c7bf4-e85e-444f-999e-af9b10c2b9f8  Content-Type: text/xml; charset=utf-8    <?xml version='1.0' encoding='UTF-8'?><S:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:S=""http://schemas.xmlsoap.org/soap/envelope/""><S:Body><ns0:traKQuaGDichResponse xmlns:ns0=""http://kquagdich.van.gdt.gov.vn/""><ns0:return>cid:6bec049a-8c16-4156-9d58-ee1aec101d8c@example.jaxws.sun.com</ns0:return></ns0:traKQuaGDichResponse></S:Body></S:Envelope>  --uuid:901c7bf4-e85e-444f-999e-af9b10c2b9f8  Content-Id:<6bec049a-8c16-4156-9d58-ee1aec101d8c@example.jaxws.sun.com>  Content-Type: application/octet-stream  Content-Transfer-Encoding: binary    <?xml version=""1.0"" encoding=""UTF-8""?><TBaoThueDTu xmlns=""http://kekhaithue.gdt.gov.vn/TBaoThue"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">     <TBaoThue id=""_NODE_TO_SIGN"">        <TTinChung>           <CQT>              <maCQT>00000</maCQT>              <tenCQT>Tổng Cục Thuế</tenCQT>              <DVu>                 <maDVu>IHTKK</maDVu>                 <tenDVu>IHTKK</tenDVu>                 <pbanDVu>3.1.0</pbanDVu>              </DVu>           </CQT>           <NNhanTBaoThue>              <maNNhan>0000000000</maNNhan>              <tenNNhan/>              <diaChiNNhan/>           </NNhanTBaoThue>           <TTinTBaoThue>              <maTBao>213</maTBao>              <tenTBao>Giao dịch không hợp lệ</tenTBao>              <pbanTBao>2.0.9</pbanTBao>              <soTBao/>              <ngayTBao>2019-03-09</ngayTBao>           </TTinTBaoThue>        </TTinChung>        <NDungTBao>           <GDich>              <mauLoaiThongBao/>              <tenLoaiThongBao>Giao dịch không hợp lệ</tenLoaiThongBao>              <ngayGDich>09/03/2019</ngayGDich>              <maGDich>00010-0002-0903201910453447-42480</maGDich>              <maLoaiGDich>####</maLoaiGDich>              <ndungGDich>Đăng ký hồ sơ khai thuế nộp qua dịch vụ VAN</ndungGDich>              <maKQuaGDich>03</maKQuaGDich>              <kquaGDich>Không chấp nhận</kquaGDich>              <maLoiGDich>IHTKK-9999</maLoiGDich>              <mtaLoiGDich>Tờ khai không đúng định dạng với XSD: Premature end of file.</mtaLoiGDich>           </GDich>           <CTietGDich>              <maGDC/>           </CTietGDich>        </NDungTBao>     </TBaoThue>  <CKyDTu><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments""/><SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1""/><Reference URI=""#_NODE_TO_SIGN""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature""/></Transforms><DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1""/><DigestValue>Had/Pq/vkvTdbgJskVzozbAzyaM=</DigestValue></Reference></SignedInfo><SignatureValue>Eb1e6+v1BqmYKaQBvnMJoZ0kWfjbDe5Dyp4I753LafsWnQghXp4KLj6CtBteUGse+fX605Doyk3Y  gUJWOPx0siTEJXlf3psmmMoHuwr5aTeOr+Csx1mSw1I6seseOxlCpovkXrZkZaW7ZfvI4FonaR8Y  d6O8AZHyXIYRaFAe/i0TS+JrLsRLu5Pwklvp3Jc9tXB1K1qPFKNKEZgGcrCOK2Fh19Bhlqb4qpbV  O5hmK3pM/RKOnHNwIUmIG0O5fViAUri2/o1mEPILl52F4WDlZcDCafXZ1E9yKt1IBS/oYeeQB/gZ  LUTsM/XzgN/3JlVIh+dpB1ywkSlVLdZ+EQwqcg==</SignatureValue><KeyInfo><KeyValue><RSAKeyValue><Modulus>kKg/XBu1Cex/Q36/m9/9YISS69FpfFsZX9NhPwwjOJG6t7SUqZU5C82TMc2VzcF8w0V6SzvA3evB  j52TqFQG06jhVpfoilFaJaav3OnM80yDnmJnyEVVAA9iSIDbpxAm3dDP945x1hOYxTId7gMYqyL7  29qEAbUgpuFrdFwKHYsQp5TnBq0zZG7nZaQiHbTIUVaCG/fFtdM2IhVFG5+tzeFF6FrzHyd1Du22  AyoD7PrkPsQsc3cEPLjZHtEulQTxiz36+WcVh6OQWZLo+0dBDXwVI9Tb2c/zaHbK7AoQZ2O2iNrm  nsKO0RlEn0s+5Slfe9jEA0kTjXqWom6etqKYhQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue></KeyValue><X509Data><X509SubjectName>C=VN,L=Sô  123 Lò Đúc \, Hai Bà Trưng\, Hà Nội,O=MST:0100231226,CN=Tổng  cục Thuế</X509SubjectName><X509Certificate>MIIFMTCCBBmgAwIBAgIQVAEBBHBaECcrI/rg5a8qUjANBgkqhkiG9w0BAQUFADBuMQswCQYDVQQG  EwJWTjEYMBYGA1UEChMPRlBUIENvcnBvcmF0aW9uMR8wHQYDVQQLExZGUFQgSW5mb3JtYXRpb24g  U3lzdGVtMSQwIgYDVQQDExtGUFQgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkwHhcNMTkwMjIyMTQ1  NjMzWhcNMjAxMDIxMDM0NDU3WjB+MR0wGwYDVQQDDBRU4buVbmcgIGPhu6VjIFRodeG6vzEXMBUG  A1UECgwOTVNUOjAxMDAyMzEyMjYxNzA1BgNVBAcMLlPDtCAgMTIzIEzDsiDEkMO6YyAsIEhhaSBC  w6AgVHLGsG5nLCBIw6AgTuG7mWkxCzAJBgNVBAYTAlZOMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A  MIIBCgKCAQEAkKg/XBu1Cex/Q36/m9/9YISS69FpfFsZX9NhPwwjOJG6t7SUqZU5C82TMc2VzcF8  w0V6SzvA3evBj52TqFQG06jhVpfoilFaJaav3OnM80yDnmJnyEVVAA9iSIDbpxAm3dDP945x1hOY  xTId7gMYqyL729qEAbUgpuFrdFwKHYsQp5TnBq0zZG7nZaQiHbTIUVaCG/fFtdM2IhVFG5+tzeFF  6FrzHyd1Du22AyoD7PrkPsQsc3cEPLjZHtEulQTxiz36+WcVh6OQWZLo+0dBDXwVI9Tb2c/zaHbK  7AoQZ2O2iNrmnsKO0RlEn0s+5Slfe9jEA0kTjXqWom6etqKYhQIDAQABo4IBuTCCAbUwgZ4GCCsG  AQUFBwEBBIGRMIGOMDcGCCsGAQUFBzAChitodHRwOi8vcHVibGljLnJvb3RjYS5nb3Yudm4vY3J0  L21pY25yY2EuY3J0MC8GCCsGAQUFBzAChiNodHRwOi8vd3d3LmZpcy5jb20udm4vY3J0L2ZwdGNh  LmNydDAiBggrBgEFBQcwAYYWaHR0cDovL29jc3AuZmlzLmNvbS52bjAdBgNVHQ4EFgQUM2zXoZGS  W2pAyzyY2kUPQRfwYQAwDAYDVR0TAQH/BAIwADAfBgNVHSMEGDAWgBSz3nGSHEUdyZtP9xeVU86i  TetZMjAZBgNVHSAEEjAQMA4GDCsGAQQBge0DAQQBBjAtBgNVHR8BAf8EIzAhMB+gHaAbhhlodHRw  Oi8vY3JsLmZpcy5jb20udm4vZ2V0MA4GA1UdDwEB/wQEAwIB/jBqBgNVHSUBAf8EYDBeBggrBgEF  BQcDAQYIKwYBBQUHAwIGCCsGAQUFBwMDBggrBgEFBQcDBAYIKwYBBQUHAw8GCCsGAQUFBwMQBggr  BgEFBQcDEQYKKwYBBAGCNxQCAgYKKwYBBAGCNwoDDDANBgkqhkiG9w0BAQUFAAOCAQEAPlnTg4d9  rdl/FaKEFPXAiR+Jwijy8xckQJ17bzxaOkDZxvFSMirzHC3OT5Hd69QyPTK4L9QSNEsgELJIsvDV  KVthqJsco+nwqrqM2v85prMHrOonbGIxPWnW1xqYj9Zprj/QM9chgAZFT3v2fYejiA5kaevAQny3  3/mJrG62uNKlZ50aGB2Y0ew8LsMucHLau56OXn1armm0C0Egqtv8thTFXvUuM+uo7m2A+w7jjG4g  lreG7x//9rDONEwadkamF8z65ysQfkYkKLt2C9/x0ji3SpCml0xG+IWZBFYh8VFrUEShekVDVtCN  Ulaxrgftmjg6AK/tgHfb1GqUVvPvOw==</X509Certificate></X509Data></KeyInfo></Signature></CKyDTu></TBaoThueDTu>  --uuid:901c7bf4-e85e-444f-999e-af9b10c2b9f8--";
				int Pos1 = strXML.IndexOf("<TBaoThueDTu xmlns");
				int Pos2 = strXML.IndexOf("</TBaoThueDTu>") + ("</TBaoThueDTu>").Length;
				string strFinalString = strXML.Substring(Pos1, Pos2 - Pos1);

				string strResult = strFinalString;

				DataSet ds = CmUtils.XmlUtils.Xml2DataSet(strResult);
			}
			#endregion
		}

		private void TestMix_01_Std()
		{
			////
			bool bTest = false;

			if (bTest)
			{
				object objYear = 2018;

				string strDate = TUtils.CUtils.StdDateBeginOfYear(objYear);
			}

		}


		private void TestMix_HuongTa_SplitString()
		{
			string strFinalRemark = @"VS057-TTBL-1911PMT44216-117MD192 - 6600242-015499 85PT (VTC). - TU: AUTO TRUONG CHINH CORP";
			string[] arrrSessionId = strFinalRemark.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
			if(arrrSessionId.Length > 2)
			{
				string strDealerCode = TUtils.CUtils.StdParam(arrrSessionId[0]);
				string strPaymentType = TUtils.CUtils.StdParam(arrrSessionId[1]);
				string strPaymentCode = TUtils.CUtils.StdParam(arrrSessionId[2]);
			}
			//string strSessionId = strFinalRemark.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0];
		}

		private void TestMix_01_String()
		{
			List<string> listDate = new List<string>();

			listDate.Add("2019-06-21");
			listDate.Add("2019-06-19");
			listDate.Add("2019-06-20");
			listDate.Add("2019-06-29");

			List<string> listDateSort = listDate.OrderByDescending(x => DateTime.Parse(x)).ToList();
		}

		private void TestMix_02_GetJson()
		{
			bool bTest = false;
			string strJson = null;

			var objRQ_Sys_User = new RQ_Sys_User();

			var objRQ_Sys_User_Json = "";

			//objRQ_Sys_User.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
			//objRQ_Sys_User.GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
			//objRQ_Sys_User.GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
			//objRQ_Sys_User.WAUserCode = "SYSADMIN";
			//objRQ_Sys_User.WAUserPassword = "1";
			//objRQ_Sys_User.Ft_RecordStart = "0";
			//objRQ_Sys_User.Ft_RecordCount = "12345600";
			//objRQ_Sys_User.Ft_WhereClause = "";
			//objRQ_Sys_User.Rt_Cols_Sys_User = "*";
			//objRQ_Sys_User.Rt_Cols_Sys_UserInGroup = "*";
			//objRQ_Sys_User.Lst_Sys_User = new List<Sys_User>();

			//Sys_User obj_Sys_User_01 = new Sys_User();
			//obj_Sys_User_01.UserCode = "DUNGND";
			//obj_Sys_User_01.BankCode = "ALL";
			//obj_Sys_User_01.UserNick = "DUNGND";
			//obj_Sys_User_01.UserPassword = "1";
			//obj_Sys_User_01.FlagSysAdmin = "1";
			//obj_Sys_User_01.FlagActive = "1";

			//objRQ_Sys_User.Lst_Sys_User.Add(obj_Sys_User_01);

			//DataTable dataTable = TUtils.DataTableCmUtils.ToDataTable(objRQ_Sys_User.Lst_Sys_User, "Sys_User");


			//var objRQ_Sys_User_Json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);

			#region // WA_Sys_User_GetForCurrentUser:
			if (bTest)
			{
				//var objRQ_Sys_User = new RQ_Sys_User();

				objRQ_Sys_User.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
				objRQ_Sys_User.GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.WAUserCode = "SYSADMIN";
				objRQ_Sys_User.WAUserPassword = "123456";
				////

				objRQ_Sys_User_Json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);

				//{"Rt_Cols_Sys_User":null,"Rt_Cols_Sys_UserInGroup":null,"Sys_User":null,"Tid":"20181012.103518.721831","GwUserCode":"idocNet.idn.Skycic.Inventory.Sv","GwPassword":"idocNet.idn.Skycic.Inventory.Sv","WAUserCode":"SYSADMIN","WAUserPassword":"123456","FuncType":null,"Ft_RecordStart":null,"Ft_RecordCount":null,"Ft_WhereClause":null,"Ft_Cols_Upd":null}
			}
			#endregion

			#region // WA_Sys_Group_Create:
			if (bTest)
			{
				var objRQ_Sys_Group = new RQ_Sys_Group();

				objRQ_Sys_Group.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
				objRQ_Sys_Group.GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_Group.GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_Group.WAUserCode = "SYSADMIN";
				objRQ_Sys_Group.WAUserPassword = "123456";
				////
				Sys_Group obj_Sys_Group_01 = new Sys_Group();
				obj_Sys_Group_01.GroupCode = "GRP.TCKT";
				obj_Sys_Group_01.GroupName = "GRP.TCKT";
				objRQ_Sys_Group.Sys_Group = obj_Sys_Group_01;

				strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_Group);

				//{ "Rt_Cols_Sys_Group":null,"Rt_Cols_Sys_UserInGroup":null,"Sys_Group":{ "GroupCode":"GRP.TCKT","GroupName":"GRP.TCKT","FlagActive":null,"LogLUDTimeUTC":null,"LogLUBy":null},"Tid":"20181012.095318.986818","GwUserCode":"idocNet.idn.Skycic.Inventory.Sv","GwPassword":"idocNet.idn.Skycic.Inventory.Sv","WAUserCode":"SYSADMIN","WAUserPassword":"123456","FuncType":null,"Ft_RecordStart":null,"Ft_RecordCount":null,"Ft_WhereClause":null,"Ft_Cols_Upd":null}
			}
			#endregion

			#region // WA_Sys_UserInGroup_Save:
			if (bTest)
			{
				//var objRQ_Sys_User = new RQ_Sys_User();

				objRQ_Sys_User.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
				objRQ_Sys_User.GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.WAUserCode = "SYSADMIN";
				objRQ_Sys_User.WAUserPassword = "123456";

				////
				Sys_Group objSys_Group = new Sys_Group();
				objSys_Group.GroupCode = "";

				objRQ_Sys_User_Json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);

				//{ "Rt_Cols_Sys_User":"*","Rt_Cols_Sys_UserInGroup":"*","Sys_User":null,"Tid":"20181011.154228.467809","GwUserCode":"idocNet.idn.Skycic.Inventory.Sv","GwPassword":"idocNet.idn.Skycic.Inventory.Sv","WAUserCode":"SYSADMIN","WAUserPassword":"123456","FuncType":null,"Ft_RecordStart":"0","Ft_RecordCount":"12345600","Ft_WhereClause":"","Ft_Cols_Upd":null}
			}
			#endregion

			#region // WA_Sys_User_Get:
			if (bTest)
			{
				//var objRQ_Sys_User = new RQ_Sys_User();

				objRQ_Sys_User.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
				objRQ_Sys_User.GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.WAUserCode = "SYSADMIN";
				objRQ_Sys_User.WAUserPassword = "123456";
				////
				objRQ_Sys_User.Ft_RecordStart = "0";
				objRQ_Sys_User.Ft_RecordCount = "12345600";
				objRQ_Sys_User.Ft_WhereClause = "";
				objRQ_Sys_User.Rt_Cols_Sys_User = "*";
				objRQ_Sys_User.Rt_Cols_Sys_UserInGroup = "*";

				objRQ_Sys_User_Json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);

				//{ "Rt_Cols_Sys_User":"*","Rt_Cols_Sys_UserInGroup":"*","Sys_User":null,"Tid":"20181011.154228.467809","GwUserCode":"idocNet.idn.Skycic.Inventory.Sv","GwPassword":"idocNet.idn.Skycic.Inventory.Sv","WAUserCode":"SYSADMIN","WAUserPassword":"123456","FuncType":null,"Ft_RecordStart":"0","Ft_RecordCount":"12345600","Ft_WhereClause":"","Ft_Cols_Upd":null}
			}
			#endregion

			#region // WA_Sys_User_Create:
			if (bTest)
			{
				//var objRQ_Sys_User = new RQ_Sys_User();

				objRQ_Sys_User.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
				objRQ_Sys_User.GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.WAUserCode = "SYSADMIN";
				objRQ_Sys_User.WAUserPassword = "123456";
				////
				Sys_User obj_Sys_User_01 = new Sys_User();
				obj_Sys_User_01.UserCode = "DUNGND";
				obj_Sys_User_01.UserName = "DUNGND";
				obj_Sys_User_01.UserPassword = "1";
				obj_Sys_User_01.PhoneNo = "1";
				obj_Sys_User_01.EMail = "dungnd@idocnet.com";
				obj_Sys_User_01.OrganCode = "TCG";
				obj_Sys_User_01.DepartmentCode = "TAICHINHKETOAN.TCG";
				obj_Sys_User_01.Position = "IT";
				obj_Sys_User_01.FlagSysAdmin = "1";
				obj_Sys_User_01.FlagActive = "1";
				objRQ_Sys_User.Sys_User = obj_Sys_User_01;

				objRQ_Sys_User_Json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);

				// { "Rt_Cols_Sys_User":null,"Rt_Cols_Sys_UserInGroup":null,"Sys_User":{ "UserCode":"DUNGND","UserName":"DUNGND","UserPassword":"1","PhoneNo":"1","EMail":"dungnd@idocnet.com","OrganCode":"TCG","DepartmentCode":"TAICHINHKETOAN.TCG","Position":"IT","FlagSysAdmin":"1","FlagActive":"1","LogLUDTimeUTC":null,"LogLUBy":null},"Tid":"20181011.153619.781366","GwUserCode":"idocNet.idn.Skycic.Inventory.Sv","GwPassword":"idocNet.idn.Skycic.Inventory.Sv","WAUserCode":"SYSADMIN","WAUserPassword":"123456","FuncType":null,"Ft_RecordStart":null,"Ft_RecordCount":null,"Ft_WhereClause":null,"Ft_Cols_Upd":null}
			}
			#endregion

			#region // WA_Sys_User_Login:
			if (bTest)
			{
				//var objRQ_Sys_User = new RQ_Sys_User();

				objRQ_Sys_User.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
				objRQ_Sys_User.GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
				objRQ_Sys_User.WAUserCode = "SYSADMIN";
				objRQ_Sys_User.WAUserPassword = "123456";

				objRQ_Sys_User_Json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);
			}
			#endregion
		}

		private void TestMix_DTimeRange()
		{
			// //
			bool bTest = false;

			DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
			dtfi.ShortDatePattern = "yyyy-MM-dd";
			dtfi.DateSeparator = "-";

			string strBSDate = "2018-11-01";
			string strBEDate = "2018-11-30";

			//string strPSDate = "2018-10-01";
			//string strPEDate = "2018-10-31";

			//string strPSDate = "2018-12-01";
			//string strPEDate = "2018-12-31";

			//string strPSDate = "2018-10-10";
			//string strPEDate = "2018-11-10";

			// case 3
			//string strPSDate = "2018-11-10";
			//string strPEDate = "2018-11-20";

			// case 4
			string strPSDate = "2018-11-10";
			string strPEDate = "2018-12-20";

			#region // GetNumOfOverLappingDays:
			if (bTest)
			{

				double dblOverLappingDays = CalcNumDaysOfOverLap(
					Convert.ToDateTime(strBSDate, dtfi) // BS
					, Convert.ToDateTime(strBEDate, dtfi) // BE
					, Convert.ToDateTime(strPSDate, dtfi) // PS
					, Convert.ToDateTime(strPEDate, dtfi) // PE
					);
			}
			#endregion

			#region // GetNumOfOverLappingDays:
			if (bTest)
			{
				
				double dblOverLappingDays = GetNumOfOverLappingDays(
					Convert.ToDateTime(strBSDate, dtfi) // BS
					, Convert.ToDateTime(strBEDate, dtfi) // BE
					, Convert.ToDateTime(strPSDate, dtfi) // PS
					, Convert.ToDateTime(strPEDate, dtfi) // PE
					);
			}
			#endregion
		}

		private static double GetNumOfOverLappingDays(DateTime BS, DateTime BE, DateTime PS, DateTime PE)
		{
			//case 1:
			//                  |--- B ---|
			//                  |----P ---|

			//case 2:
			//                  |--- B ---|
			//                          | --- P --- |

			//case 3:
			//                  |--- B ---|
			//          | --- P ---- |

			//case 4:
			//                  |--- B ---|
			//                     | - P - |

			//case 5:
			//                  |--- B ---|
			//              | -------- P -------- |

			double days = -1;

			//days = (PE - PS).TotalDays;

			bool isNotOverLap = (BS > PE) || (PS > BE);

			if (isNotOverLap == false)
			{
				//case 1
				if (BS == PS && BS == PE)
				{
					days = (PE - PS).TotalDays;
				}
				//case 2
				else if (BE > PS && BE < PE)
				{
					days = (BE - PS).TotalDays;
				}
				//case 3
				else if (BS > PS && BS < PE)
				{
					//TimeSpan t = PE - BS;
					days = (PE - BS).TotalDays;
				}
				//case 4
				else if (BS < PS && BE > PE)
				{
					days = (PE - PS).TotalDays;
				}
				//case 5
				else if (BS > PS && BE < PE)
				{
					days = (BE - PS).TotalDays;
				}
			}
			return days;
		}

		private static readonly string CallXWeb_HttpWebReq_ContentType = "application/x-www-form-urlencoded";
		private static readonly string CallXWeb_HttpWebReq_Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
		private const int CallXWeb_HttpWebReq_Timeout = 123456000; // milliseconds

		private void Test_Mix_CallWebAPI_01()
		{
			bool bTest = false;

			string strURL = "http://localhost:1400/api/MasterServer/WA_MstSv_Inos_User_Activate";
			//string strWebAPIUrl = "http://localhost:1400";

			RQ_MstSv_Inos_User objRQ_MstSv_Inos_User = new RQ_MstSv_Inos_User();

			MstSv_Inos_User objMstSv_Inos_User = new MstSv_Inos_User();

			objMstSv_Inos_User.MST = "2802532228";
			objMstSv_Inos_User.Email = "support@idocnet.com";

			objRQ_MstSv_Inos_User.MstSv_Inos_User = objMstSv_Inos_User;

			#region // HttpClient:
			if (bTest)
			{
				//var result = PostData< RT_MstSv_Inos_User, RQ_MstSv_Inos_User>(strWebAPIUrl, "MasterServer", "WA_MstSv_Inos_User_Activate", new { }, objRQ_MstSv_Inos_User);

			}
			#endregion

			#region // Http Request:
			if (bTest)
			{
				HttpWebRequest httpWebReq = (HttpWebRequest)WebRequest.Create(strURL);
				httpWebReq.Method = "POST";
				httpWebReq.ContentType = CallXWeb_HttpWebReq_ContentType;
				httpWebReq.Accept = CallXWeb_HttpWebReq_Accept;
				httpWebReq.Timeout = CallXWeb_HttpWebReq_Timeout;

				//RQ_MstSv_Inos_User objRQ_MstSv_Inos_User = new RQ_MstSv_Inos_User();

				//MstSv_Inos_User objMstSv_Inos_User = new MstSv_Inos_User();

				//objMstSv_Inos_User.MST = "2802532228";
				//objMstSv_Inos_User.Email = "support@idocnet.com";

				//objRQ_MstSv_Inos_User.MstSv_Inos_User = objMstSv_Inos_User;

				string strData = TJson.JsonConvert.SerializeObject(objRQ_MstSv_Inos_User);
				byte[] arrbyData = Encoding.UTF8.GetBytes(strData);

				using (var stream = httpWebReq.GetRequestStream())
				{
					stream.Write(arrbyData, 0, arrbyData.Length);
					stream.Close();
				}

				// // SendRequest and GetResponse:
				string strOut = null;
				using (HttpWebResponse httpWebResp = (HttpWebResponse)httpWebReq.GetResponse())
				{
					using (StreamReader streamReader = new StreamReader(httpWebResp.GetResponseStream()))
					{
						strOut = streamReader.ReadToEnd();
						streamReader.Close();
					}
				}
			}
			#endregion

		}


		private void Test_Mix_Code36_01()
		{
			bool bTest = false;
			string strOrgID = "21";
			string strYear = "3000";
			string strMonth = "7";
			string strDay = "9";

			string strRandom = "999999";
			string strChecksum = "1";

			string strFormat = "{0}{1}{2}{3}{5}";
			string strFormatFinal = "{0}{1}{2}{3}{4}{5}{6}";


			#region // PassWordHash:
			if (bTest)
			{
				string strPwInput = "123456";
				string strPwHash = TUtils.CUtils.GetEncodedHash(strPwInput); // xIqkU16EqzfPnlxkr3PbKA.
			}
			#endregion

			#region // InvoiceCode:
			if (bTest)
			{
				string strOrgID36 = TUtils.CMyBase36.To36(Convert.ToInt32(strOrgID), 4);
				string strYear36 = TUtils.CMyBase36.To36(Convert.ToInt32(strYear), 2);
				string strMonth36 = TUtils.CMyBase36.To36(Convert.ToInt32(strMonth), 2);
				string strDay36 = TUtils.CMyBase36.To36(Convert.ToInt32(strDay), 2);
				string strRandom36 = TUtils.CMyBase36.To36(Convert.ToInt32(strRandom), 7);
				string strChecksum36 = TUtils.CMyBase36.To36(Convert.ToInt32(strChecksum), 1);

				string strResult = "";
				strResult = string.Format(
					strFormat // Format
					, strOrgID36 // {0}
					, strYear36 // {1}
					, strRandom36 // {2}
					);

				////
				List<int> lstChar = new List<int>();
				for (int nScan = 0; nScan < strResult.Length; nScan++)
				{
					////
					char objchar = strResult[nScan];

					int nchar = TUtils.CMyBase36.To10(Convert.ToString(objchar));
					lstChar.Add(nchar);

				}

				Int64 res = lstChar.AsQueryable().Sum();

				//res = 190000;

				string mod = Convert.ToString(res % 36);
				Int32 nMod = Convert.ToInt32(mod);

				string strResultFinal = "";
				strResultFinal = string.Format(
					strFormatFinal // Format
					, strOrgID36 // {0}
					, strYear36 // {1}
					, strRandom36 // {2}
					, TUtils.CMyBase36.To36(nMod)
					);

			}
			#endregion
		}

		private double CalcNumDaysOfOverLap(
			DateTime BSDTime
			, DateTime BEDTime
			, DateTime PSDTime
			, DateTime PEDTime
			)
		{
			//case 1:
			//                  |--- B ---|
			//                  |----P ---|

			//case 2:
			//                  |--- B ---|
			//                          | --- P --- |

			//case 3:
			//                  |--- B ---|
			//          | --- P ---- |

			//case 4:
			//                  |--- B ---|
			//                     | - P - |

			//case 5:
			//                  |--- B ---|
			//              | -------- P -------- |

			double dblDays = -1;
			bool isNotOverLap = (BSDTime > PEDTime) || (PSDTime > BEDTime);

			if (isNotOverLap == false)
			{
				//case 0
				if (BSDTime == PSDTime && BSDTime == PEDTime)
				{
					dblDays = (PEDTime - PSDTime).TotalDays;
				}
				//case 2
				else if (BSDTime > PSDTime & BSDTime < PEDTime & BEDTime > PEDTime)
				{
					dblDays = (PEDTime - BSDTime).TotalDays;
				}
				//case 3
				else if (BSDTime < PSDTime & BEDTime > PEDTime)
				{
					dblDays = (PEDTime - PSDTime).TotalDays;
				}
				//case 4
				else if (BSDTime < PSDTime & BEDTime < PEDTime)
				{
					dblDays = (BEDTime - PSDTime).TotalDays;
				}
				//case 6
				else if (BSDTime > PSDTime & BEDTime < PEDTime & BSDTime > PSDTime & BEDTime < PEDTime)
				{
					dblDays = (BEDTime - BSDTime).TotalDays;
				}
				////case 3
				//else if (BSDTime > PSDTime && BSDTime < PEDTime)
				//{
				//	dblDays = (PEDTime - BSDTime).TotalDays;
				//}
				////case 4
				//else if (BSDTime < PSDTime && BEDTime > PEDTime)
				//{
				//	dblDays = (PEDTime - PSDTime).TotalDays;
				//}
				////case 5
				//else if (BSDTime > PSDTime && BEDTime < PEDTime)
				//{
				//	dblDays = (BEDTime - PSDTime).TotalDays;
				//}
			}


			return (double)dblDays;
		}

		private static double GetNumOfOverLappingDays_New(DateTime BS, DateTime BE, DateTime PS, DateTime PE)
		{
			//case 1:
			//                  |--- B ---|
			//                  |----P ---|

			//case 2:
			//                  |--- B ---|
			//                          | --- P --- |

			//case 3:
			//                  |--- B ---|
			//          | --- P ---- |

			//case 4:
			//                  |--- B ---|
			//                     | - P - |

			//case 5:
			//                  |--- B ---|
			//              | -------- P -------- |

			double days = -1;

			//days = (PE - PS).TotalDays;

			bool isNotOverLap = (BS > PE) || (PS > BE);

			if (isNotOverLap == false)
			{
				//case 1
				if (BS == PS && BS == PE)
				{
					days = (PE - PS).TotalDays;
				}
				//case 2
				else if (BS > PS & BE < PE & BS > PS)
				{
					days = (PS - BE).TotalDays;
				}
				////case 3
				//else if (BS > PS && BS < PE)
				//{
				//	//TimeSpan t = PE - BS;
				//	days = (PE - BS).TotalDays;
				//}
				////case 4
				//else if (BS < PS && BE > PE)
				//{
				//	days = (PE - PS).TotalDays;
				//}
				////case 5
				//else if (BS > PS && BE < PE)
				//{
				//	days = (BE - PS).TotalDays;
				//}
			}
			return days;
		}

		private void TestMix_DateTimeUTC()
		{
			bool bTest = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strdtimeInput = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			#region // Get Offset:
			if (bTest)
			{
				var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalHours;
			}
			#endregion

			#region // Convert to Utc:
			if (bTest)
			{
				var strDTimeUTC = DateTimeOffset.Parse(strdtimeInput).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
			}
			#endregion

			#region // UTCNow:
			if (bTest)
			{
				string strdtimeSys = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
			}
			#endregion

			#region // UTCNow => GMT+7:
			if (bTest)
			{
				string strdtimeSys= dtimeSys.AddHours(-7).ToString("yyyy-MM-dd HH:mm:ss");
			}
			#endregion
		}

		private void TestMix_02_GetSimpleHash()
		{
			string str5 = TUtils.CUtils.GetSimpleHash("hnvang"); // "hnvang" => 1817260620

			string str0 = TUtils.CUtils.GetSimpleHash(""); // null
			string str1 = TUtils.CUtils.GetSimpleHash("1"); // "5001010101"
			string str2 = TUtils.CUtils.GetSimpleHash("12"); // "5153020202"
			string str3 = TUtils.CUtils.GetSimpleHash("123"); // "5254560303"
			string str4 = TUtils.CUtils.GetSimpleHash("123456"); // "1457596163"
			// //
			/// update t set t.UserPassword = '1457596163' from Sys_User t; -- '123456'
			// //
		}
		#endregion

		private void ZTest_Load(object sender, EventArgs e)
		{

		}

		#region // TestBank:
		private void btnTestWSBank_Click(object sender, EventArgs e)
		{
			
		}
		#endregion

		#region // Test TVAN:
		private void TestMix_01_NhanHSoThue()
		{
			bool bTest = false;

			vn.gov.gdt.daotaonhantokhai.NhanHSoThueService.NhanHSoThueService wsNhanHSoThueService = new vn.gov.gdt.daotaonhantokhai.NhanHSoThueService.NhanHSoThueService();
			vn.gov.gdt.daotaonhantokhai.KQuaGDichService.KQuaGDichService wsKQuaGDichService = new vn.gov.gdt.daotaonhantokhai.KQuaGDichService.KQuaGDichService();

			#region // maSoThue:
			if (bTest)
			{
				string maSoThue = "0129877782832";

				var var = wsKQuaGDichService.traTThaiMaSoThue(maSoThue);
			}
			#endregion

		}

		private void TestMix_02_KQuaGDich()
		{
			bool bTest = false;
			string userName = "00010";
			string password = "hilo123";

			vn.gov.gdt.daotaonhantokhai.NhanHSoThueService.NhanHSoThueService wsNhanHSoThueService = new vn.gov.gdt.daotaonhantokhai.NhanHSoThueService.NhanHSoThueService();
			vn.gov.gdt.daotaonhantokhai.KQuaGDichService.KQuaGDichService wsKQuaGDichService = new vn.gov.gdt.daotaonhantokhai.KQuaGDichService.KQuaGDichService();


			#region // maSoThue: TC01.
			if (bTest)
			{

				//wsKQuaGDichService.Credentials = new ClientCredentials();
				string maSoThue = "0129877782832";
				//wsKQuaGDichService.Url = @"http://daotaonhantokhai.gdt.gov.vn/ihtkk_van/KQuaGDichServicePort?WSDL";
				wsKQuaGDichService.Url = @"http://daotaonhantokhai.gdt.gov.vn:80/ihtkk_van/KQuaGDichServicePort";
				string strResult = wsKQuaGDichService.traTThaiMaSoThue(maSoThue);
			}
			#endregion

			#region // maSoThue: TC01.
			if (bTest)
			{
				
				wsKQuaGDichService.Credentials = new NetworkCredential(userName, password);
				string maSoThue = "0129877782832";
				//wsKQuaGDichService.Url = @"http://daotaonhantokhai.gdt.gov.vn/ihtkk_van/KQuaGDichServicePort?WSDL";
				wsKQuaGDichService.Url = @"http://daotaonhantokhai.gdt.gov.vn:80/ihtkk_van/KQuaGDichServicePort";
				string strResult = wsKQuaGDichService.traTThaiMaSoThue(maSoThue);
			}
			#endregion

		}

		private void TestMix_02_WSJX_KQuaGDich()
		{
			bool bTest = false;
			string userName = "00010";
			string password = "hilo123";

			#region // traKQuaGDich:
			if (bTest)
			{
				string maGDich = "00010-0002-08032019102358983-42469";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var result = ws.traKQuaGDich(maGDich);
			}
			#endregion

			#region // traTTinMaSoThue:
			if (bTest)
			{
				string strMaSoThue = "0102454468";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var ret = ws.traTTinMaSoThue(strMaSoThue);
			}
			#endregion

			#region // traKQuaGDich:
			if (bTest)
			{
				string maGDich = "00010-0002-08032019102358983-42469";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var result = ws.traKQuaGDich(maGDich);
			}
			#endregion

			#region // traTTinMaSoThue:
			if (bTest)
			{
				string strMaSoThue = "0102454468";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var ret = ws.traTThaiMaSoThue(strMaSoThue);
			}
			#endregion

			#region // traTTinMaSoThue:
			if (bTest)
			{
				string strMaSoThue = "66777777";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var ret = ws.traTTinMaSoThue(strMaSoThue);
			}
			#endregion

		}

		private void TestMix_02_SOAP_NhanHSoThueService()
		{
			bool bTest = false;
			string userName = "00010";
			string password = "hilo123";

			#region // traKQuaGDich:
			if (bTest)
			{
				string maGDich = "00010-0002-08032019102358983-42469";
				string dlieuHSo = "<DKyThueDTu xmlns=\"http://kekhaithue.gdt.gov.vn/HSoDKy\">   <DKyThue id=\"_NODE_TO_SIGN\">    <TTinChung>     <CQT>      <maCQT>70125</maCQT>      <tenCQT>Chi cục Thuế Quận Gò Vấp</tenCQT>      <DVu>       <maDVu>0002</maDVu>       <tenDVu>BKAV</tenDVu>       <soGPhepKDoanh>15/GCN-TCT</soGPhepKDoanh>      </DVu>     </CQT>     <TTinDKyThue>      <maDKy>216</maDKy>      <mauDKy>02-DK_T-VAN</mauDKy>      <tenDKy>Đăng ký sử dụng dịch vụ TVAN</tenDKy>      <pBanDKy>2.0.9</pBanDKy>      <ngayDKy>2018-07-17</ngayDKy>      <tIN>0303114366</tIN>      <tenNNT>CÔNG TY TNHH KỸ THUẬT - THƯƠNG MẠI HUY THY</tenNNT>     </TTinDKyThue>    </TTinChung>    <NDungDKy>     <diaDiemTB></diaDiemTB>      <issuer>BkavCA</issuer>     <subject>CÔNG TY TNHH KỸ THUẬT - THƯƠNG MẠI HUY THY</subject>     <serial>540329bcca349319b6e08e03a80947bb</serial>     <email>congtyhuythy2017@gmail.com</email>     <tel>0909971400,0901397340,02822536670</tel>     <fromDateCA>16/07/2018 16:19:13</fromDateCA>     <toDateCA>15/05/2020 10:35:48</toDateCA>     <dkyTDT>true</dkyTDT>     <kkTDT>true</kkTDT>     <tenToChuc>BkavCA</tenToChuc>     <soGiayCNhan>15/GCN-TCT</soGiayCNhan>    </NDungDKy>   </DKyThue>  <CKyDTu><ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\"><ds:SignedInfo><ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments\"></ds:CanonicalizationMethod><ds:SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\"></ds:SignatureMethod><ds:Reference URI=\"#_NODE_TO_SIGN\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"></ds:Transform></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"></ds:DigestMethod><ds:DigestValue>5QlfNpHFVqGzQa0AFt6k3AQmwo8=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>VZNEp0bC7XebJiogH0PL2AGQlB1QJhD/4P1/WKndFcZPYOHuqB2oEZMlTsiNPG9siSiYTc/2v9cHXrVDF8TozXU8eu7kobNrEbgXdMWZF3qcgStFBlm862vFrwwPKOOVt6w7PUdmRWefBr15RCqZMOYUXqAPV/6PtGN+qyYbMc0=</ds:SignatureValue><ds:KeyInfo><ds:KeyValue><ds:RSAKeyValue><ds:Modulus>qq/Ep2z6SZGNoIgQM4vVKhxAaZj6nz1+P2xH0mINHJOsJaPBHCAwWRaP8WwdAR5nPZc1qqv9SfeSG34uzfRVYHO+ljQATTVqP8cgqTG9cBtdpcUBR6hCc4XGP2tATP38heWYxB1/BI+CrsSnSjYO+QozytMKCVFaGDC6YqwMxls=</ds:Modulus><ds:Exponent>AQAB</ds:Exponent></ds:RSAKeyValue></ds:KeyValue><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=BkavCA, O=Bkav Corporation, L=Hanoi, C=VN</ds:X509IssuerName><ds:X509SerialNumber>111671575072776370995739170912218990523</ds:X509SerialNumber></ds:X509IssuerSerial><ds:X509SubjectName>C=VN, ST=Hồ Chí Minh, L=Gò Vấp, CN=CÔNG TY TNHH KỸ THUẬT - THƯƠNG MẠI HUY THY, UID=MST:0303114366</ds:X509SubjectName><ds:X509Certificate>MIIEITCCAwmgAwIBAgIQVAMpvMo0kxm24I4DqAlHuzANBgkqhkiG9w0BAQUFADBJMQswCQYDVQQGEwJWTjEOMAwGA1UEBxMFSGFub2kxGTAXBgNVBAoTEEJrYXYgQ29ycG9yYXRpb24xDzANBgNVBAMTBkJrYXZDQTAeFw0xODA3MTYwOTE5MTNaFw0yMDA1MTUwMzM1NDhaMIGYMR4wHAYKCZImiZPyLGQBAQwOTVNUOjAzMDMxMTQzNjYxPDA6BgNVBAMMM0PDlE5HIFRZIFROSEggS+G7uCBUSFXhuqxUIC0gVEjGr8agTkcgTeG6oEkgSFVZIFRIWTESMBAGA1UEBwwJR8OyIFbhuqVwMRcwFQYDVQQIDA5I4buTIENow60gTWluaDELMAkGA1UEBhMCVk4wgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAKqvxKds+kmRjaCIEDOL1SocQGmY+p89fj9sR9JiDRyTrCWjwRwgMFkWj/FsHQEeZz2XNaqr/Un3kht+Ls30VWBzvpY0AE01aj/HIKkxvXAbXaXFAUeoQnOFxj9rQEz9/IXlmMQdfwSPgq7Ep0o2DvkKM8rTCglRWhgwumKsDMZbAgMBAAGjggE3MIIBMzAxBggrBgEFBQcBAQQlMCMwIQYIKwYBBQUHMAGGFWh0dHA6Ly9vY3NwLmJrYXZjYS52bjAdBgNVHQ4EFgQUnBdUDo4U9G4WakPtfhyk5XLZehswDAYDVR0TAQH/BAIwADAfBgNVHSMEGDAWgBQesA9Il9/Qw2enRoQ7WDuIDVOUhjB/BgNVHR8EeDB2MHSgI6Ahhh9odHRwOi8vY3JsLmJrYXZjYS52bi9Ca2F2Q0EuY3Jsok2kSzBJMQ8wDQYDVQQDDAZCa2F2Q0ExGTAXBgNVBAoMEEJrYXYgQ29ycG9yYXRpb24xDjAMBgNVBAcMBUhhbm9pMQswCQYDVQQGEwJWTjAOBgNVHQ8BAf8EBAMCBsAwHwYDVR0lBBgwFgYIKwYBBQUHAwQGCisGAQQBgjcKAwwwDQYJKoZIhvcNAQEFBQADggEBAItiUBJxp0nHDi1oOmT5hlvjS30O7+ZIaZhxpm0088mcxIvytG1lF8Pu6WPXIhPY5nLHdYpD27Dh/hbHuo9TioJR/DrOkiGvyOmStfRPNPZnmCyiZnitFiqK9bRjbScvlJ+TwoRq5/9eVjLNx/oxXPXPSnIMjsZileyLutAPJG/wQdtsQHLW0RoYrZHqCPgxdzAxrXtOMTzAOzzyWysB1OgFtrsbEgRp/wsiloVXRVJbaW3CRnQMyrwrFkF6IUWickdtihrGpIvUmkkm6vlr6oQGoeXSawwnhL55lTeEkygirR7DqVXGvvJ1GNS+Rh/viAPaXYCU9eFW2yhWN8Fw7Qs=</ds:X509Certificate></ds:X509Data></ds:KeyInfo><ds:Object><ds:SignatureProperties><ds:SignatureProperty Id=\"Timestamp\" Target=\"\"><date>17/07/2018</date><time>09:00:56</time><timezone>UTC +7</timezone></ds:SignatureProperty></ds:SignatureProperties></ds:Object></ds:Signature><Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><Reference URI=\"#_NODE_TO_SIGN\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" /></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><DigestValue>5QlfNpHFVqGzQa0AFt6k3AQmwo8=</DigestValue></Reference></SignedInfo><SignatureValue>DrdlXvWU2+h1Ktt5CTIjTRXwDL/EvCYoD5zorajvmt4m0G8bz1RkNaC5gMBCnbxpD+TXVmVhWFakR++ElAASW6xZhbiXHmRGYBWH7RcNcUFXEg9xUMYDSOW9bkVxaRlkku/E/jSBJ3XzQo2tyn6OKw23MLe2gcN00+uMXA/KXBRFjs2cCu2Zw1eYluETP90dERacfPcC+o9FUyfQR72zw0V9QnuRS3IZ2GK5vakF8vR9j8ZfFVz9i/jYDntX7lwYopWbqqL6whWV60kAE1z45imZzS5hW5lw5rjc+20i3MGOBAwQ5zrp8ZQn5L99p2cNrb1ySWIYb935AUZGVRwICw==</SignatureValue><KeyInfo><KeyValue><RSAKeyValue><Modulus>l64q8AFRN7IxR8oMAlHWCKWWb6w7sL2g/QpBpl7853wMiJ9vqo508vBZaSrvejEM+H9J0tjVaiuM3X4Qkru+Zs3pQDbvFpvXNky0FbPTwYF/P2SvIVt60e22+ZKfYpA92aeIvaoXoFt37pHMnjW1wrWuDGc/W0tQeAn07SmEOENIMP6rucqifSdGg/AujO4+PQM79H5rW1Ba8ArWRED8Vye5CS5hZXH4I15hDujn3RYBR9ylBBVMYP7+rDPGxKNVRrHv8dSkMBp3rninOtKrPVQZrOk8pr3Dnm1dh/8+6tgsrdTHJ3xDKHt3c8ZmmJaMoTYVLPAXufo3NC4TWaKFsw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue></KeyValue><X509Data><X509SubjectName>OID.0.9.2342.19200300.100.1.1=MST:0101360697, CN=CÔNG TY CỔ PHẦN BKAV, L=Cầu Giấy, S=Hà Nội, C=VN</X509SubjectName><X509Certificate>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</X509Certificate></X509Data></KeyInfo></Signature></CKyDTu></DKyThueDTu>";

				var ws = NhanHSoThueServiceProxy("", userName, password);
				var result = ws.nhanHSoThue(maGDich, dlieuHSo);
			}
			#endregion

			#region // traKQuaGDich:
			if (bTest)
			{
				string maGDich = "00010-0002-08032019102358983-42469";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var result = ws.traKQuaGDich(maGDich);
			}
			#endregion

			#region // traTTinMaSoThue:
			if (bTest)
			{
				string strMaSoThue = "0102454468";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var ret = ws.traTTinMaSoThue(strMaSoThue);
			}
			#endregion

			#region // traTTinMaSoThue:
			if (bTest)
			{
				string strMaSoThue = "0102454468";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var ret = ws.traTThaiMaSoThue(strMaSoThue);
			}
			#endregion

			#region // traTTinMaSoThue:
			if (bTest)
			{
				string strMaSoThue = "66777777";

				var ws = CreateRealTimeOnlineProxy("", userName, password);
				var ret = ws.traTTinMaSoThue(strMaSoThue);
			}
			#endregion

		}

		private void btnTVANKQuaGDich_Click(object sender, EventArgs e)
		{
			try
			{
				//TestMix_01_CallService();
				//TestMix_02_CallService();
				//TestMix_02_GetSimpleHash();
				//TestMix_02_VPBankLDAP();
				//TestMix_StringEqualIgnoreCase();
				//TestMix_02_GetJson();
				//TestMix_DateTimeUTC();
				//TestMix_DTimeRange();
				//TestMix_01_NhanHSoThue();
				//TestMix_02_SOAP_NhanHSoThueService();
				TestMix_02_WSJX_KQuaGDich();
			}
			catch (Exception exc)
			{
				CommonForms.Utils.ProcessExc(exc);
			}
		}

		public static KQuaGDichServiceClient CreateRealTimeOnlineProxy(
			string url
			, string username
			, string password)
		{
			if (string.IsNullOrEmpty(url))
				url = "http://daotaonhantokhai.gdt.gov.vn:80/ihtkk_van/KQuaGDichServicePort";

			CustomBinding binding = new CustomBinding();


			var security = TransportSecurityBindingElement.CreateUserNameOverTransportBindingElement();
			security.IncludeTimestamp = false;
			security.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic256;
			security.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
			security.AllowInsecureTransport = true; // [DLee 11-12-2013] Added
			var encoding = new TextMessageEncodingBindingElement();
			encoding.MessageVersion = MessageVersion.Soap11;

			//var transport = new HttpsTransportBindingElement();
			var transport = new HttpTransportBindingElement(); // [DLee 11-12-2013] new HttpsTransportBindingElement();
			transport.MaxReceivedMessageSize = 20000000; // 20 megs

			binding.Elements.Add(security);
			binding.Elements.Add(encoding);
			binding.Elements.Add(transport);


			KQuaGDichServiceClient client = new KQuaGDichServiceClient(binding,
				new EndpointAddress(url));

			// to use full client credential with Nonce uncomment this code:
			// it looks like this might not be required - the service seems to work without it
			client.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
			client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

			client.ClientCredentials.UserName.UserName = username;
			client.ClientCredentials.UserName.Password = password;

			return client;
		}

		public static NhanHSoThueServiceClient NhanHSoThueServiceProxy(
			string url
			, string username
			, string password)
		{
			if (string.IsNullOrEmpty(url))
				url = "http://daotaonhantokhai.gdt.gov.vn:80/ihtkk_van/NhanHSoThueServicePort";

			CustomBinding binding = new CustomBinding();


			var security = TransportSecurityBindingElement.CreateUserNameOverTransportBindingElement();
			security.IncludeTimestamp = false;
			security.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic256;
			security.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
			security.AllowInsecureTransport = true; // [DLee 11-12-2013] Added
			var encoding = new TextMessageEncodingBindingElement();
			encoding.MessageVersion = MessageVersion.Soap11;

			//var transport = new HttpsTransportBindingElement();
			var transport = new HttpTransportBindingElement(); // [DLee 11-12-2013] new HttpsTransportBindingElement();
			transport.MaxReceivedMessageSize = 20000000; // 20 megs

			binding.Elements.Add(security);
			binding.Elements.Add(encoding);
			binding.Elements.Add(transport);


			NhanHSoThueServiceClient client = new NhanHSoThueServiceClient(binding,
				new EndpointAddress(url));

			// to use full client credential with Nonce uncomment this code:
			// it looks like this might not be required - the service seems to work without it
			client.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
			client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

			client.ClientCredentials.UserName.UserName = username;
			client.ClientCredentials.UserName.Password = password;

			return client;
		}
		#endregion

		#region // Test XML:
		private void Test_XMLToDS()
		{
			bool bTest = false;

			#region // XML to DS:
			if (bTest)
			{
				string strXML = @"<DKyThueDTu xmlns=""http://kekhaithue.gdt.gov.vn/HSoDKy"">   <DKyThue id=""_NODE_TO_SIGN"">    <TTinChung>     <CQT>      <maCQT>70125</maCQT>      <tenCQT>Chi cục Thuế Quận Gò Vấp</tenCQT>      <DVu>       <maDVu>0002</maDVu>       <tenDVu>BKAV</tenDVu>       <soGPhepKDoanh>15/GCN-TCT</soGPhepKDoanh>      </DVu>     </CQT>     <TTinDKyThue>      <maDKy>216</maDKy>      <mauDKy>02-DK_T-VAN</mauDKy>      <tenDKy>Đăng ký sử dụng dịch vụ TVAN</tenDKy>      <pBanDKy>2.0.9</pBanDKy>      <ngayDKy>2018-07-17</ngayDKy>      <tIN>0303114366</tIN>      <tenNNT>CÔNG TY TNHH KỸ THUẬT - THƯƠNG MẠI HUY THY</tenNNT>     </TTinDKyThue>    </TTinChung>    <NDungDKy>     <diaDiemTB></diaDiemTB>      <issuer>BkavCA</issuer>     <subject>CÔNG TY TNHH KỸ THUẬT - THƯƠNG MẠI HUY THY</subject>     <serial>540329bcca349319b6e08e03a80947bb</serial>     <email>congtyhuythy2017@gmail.com</email>     <tel>0909971400,0901397340,02822536670</tel>     <fromDateCA>16/07/2018 16:19:13</fromDateCA>     <toDateCA>15/05/2020 10:35:48</toDateCA>     <dkyTDT>true</dkyTDT>     <kkTDT>true</kkTDT>     <tenToChuc>BkavCA</tenToChuc>     <soGiayCNhan>15/GCN-TCT</soGiayCNhan>    </NDungDKy>   </DKyThue>  <CKyDTu><ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#""><ds:SignedInfo><ds:CanonicalizationMethod Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments""></ds:CanonicalizationMethod><ds:SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1""></ds:SignatureMethod><ds:Reference URI=""#_NODE_TO_SIGN""><ds:Transforms><ds:Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature""></ds:Transform></ds:Transforms><ds:DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1""></ds:DigestMethod><ds:DigestValue>5QlfNpHFVqGzQa0AFt6k3AQmwo8=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>VZNEp0bC7XebJiogH0PL2AGQlB1QJhD/4P1/WKndFcZPYOHuqB2oEZMlTsiNPG9siSiYTc/2v9cHXrVDF8TozXU8eu7kobNrEbgXdMWZF3qcgStFBlm862vFrwwPKOOVt6w7PUdmRWefBr15RCqZMOYUXqAPV/6PtGN+qyYbMc0=</ds:SignatureValue><ds:KeyInfo><ds:KeyValue><ds:RSAKeyValue><ds:Modulus>qq/Ep2z6SZGNoIgQM4vVKhxAaZj6nz1+P2xH0mINHJOsJaPBHCAwWRaP8WwdAR5nPZc1qqv9SfeSG34uzfRVYHO+ljQATTVqP8cgqTG9cBtdpcUBR6hCc4XGP2tATP38heWYxB1/BI+CrsSnSjYO+QozytMKCVFaGDC6YqwMxls=</ds:Modulus><ds:Exponent>AQAB</ds:Exponent></ds:RSAKeyValue></ds:KeyValue><ds:X509Data><ds:X509IssuerSerial><ds:X509IssuerName>CN=BkavCA, O=Bkav Corporation, L=Hanoi, C=VN</ds:X509IssuerName><ds:X509SerialNumber>111671575072776370995739170912218990523</ds:X509SerialNumber></ds:X509IssuerSerial><ds:X509SubjectName>C=VN, ST=Hồ Chí Minh, L=Gò Vấp, CN=CÔNG TY TNHH KỸ THUẬT - THƯƠNG MẠI HUY THY, UID=MST:0303114366</ds:X509SubjectName><ds:X509Certificate>MIIEITCCAwmgAwIBAgIQVAMpvMo0kxm24I4DqAlHuzANBgkqhkiG9w0BAQUFADBJMQswCQYDVQQGEwJWTjEOMAwGA1UEBxMFSGFub2kxGTAXBgNVBAoTEEJrYXYgQ29ycG9yYXRpb24xDzANBgNVBAMTBkJrYXZDQTAeFw0xODA3MTYwOTE5MTNaFw0yMDA1MTUwMzM1NDhaMIGYMR4wHAYKCZImiZPyLGQBAQwOTVNUOjAzMDMxMTQzNjYxPDA6BgNVBAMMM0PDlE5HIFRZIFROSEggS+G7uCBUSFXhuqxUIC0gVEjGr8agTkcgTeG6oEkgSFVZIFRIWTESMBAGA1UEBwwJR8OyIFbhuqVwMRcwFQYDVQQIDA5I4buTIENow60gTWluaDELMAkGA1UEBhMCVk4wgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAKqvxKds+kmRjaCIEDOL1SocQGmY+p89fj9sR9JiDRyTrCWjwRwgMFkWj/FsHQEeZz2XNaqr/Un3kht+Ls30VWBzvpY0AE01aj/HIKkxvXAbXaXFAUeoQnOFxj9rQEz9/IXlmMQdfwSPgq7Ep0o2DvkKM8rTCglRWhgwumKsDMZbAgMBAAGjggE3MIIBMzAxBggrBgEFBQcBAQQlMCMwIQYIKwYBBQUHMAGGFWh0dHA6Ly9vY3NwLmJrYXZjYS52bjAdBgNVHQ4EFgQUnBdUDo4U9G4WakPtfhyk5XLZehswDAYDVR0TAQH/BAIwADAfBgNVHSMEGDAWgBQesA9Il9/Qw2enRoQ7WDuIDVOUhjB/BgNVHR8EeDB2MHSgI6Ahhh9odHRwOi8vY3JsLmJrYXZjYS52bi9Ca2F2Q0EuY3Jsok2kSzBJMQ8wDQYDVQQDDAZCa2F2Q0ExGTAXBgNVBAoMEEJrYXYgQ29ycG9yYXRpb24xDjAMBgNVBAcMBUhhbm9pMQswCQYDVQQGEwJWTjAOBgNVHQ8BAf8EBAMCBsAwHwYDVR0lBBgwFgYIKwYBBQUHAwQGCisGAQQBgjcKAwwwDQYJKoZIhvcNAQEFBQADggEBAItiUBJxp0nHDi1oOmT5hlvjS30O7+ZIaZhxpm0088mcxIvytG1lF8Pu6WPXIhPY5nLHdYpD27Dh/hbHuo9TioJR/DrOkiGvyOmStfRPNPZnmCyiZnitFiqK9bRjbScvlJ+TwoRq5/9eVjLNx/oxXPXPSnIMjsZileyLutAPJG/wQdtsQHLW0RoYrZHqCPgxdzAxrXtOMTzAOzzyWysB1OgFtrsbEgRp/wsiloVXRVJbaW3CRnQMyrwrFkF6IUWickdtihrGpIvUmkkm6vlr6oQGoeXSawwnhL55lTeEkygirR7DqVXGvvJ1GNS+Rh/viAPaXYCU9eFW2yhWN8Fw7Qs=</ds:X509Certificate></ds:X509Data></ds:KeyInfo><ds:Object><ds:SignatureProperties><ds:SignatureProperty Id=""Timestamp"" Target=""""><date>17/07/2018</date><time>09:00:56</time><timezone>UTC +7</timezone></ds:SignatureProperty></ds:SignatureProperties></ds:Object></ds:Signature><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315"" /><SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1"" /><Reference URI=""#_NODE_TO_SIGN""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" /></Transforms><DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1"" /><DigestValue>5QlfNpHFVqGzQa0AFt6k3AQmwo8=</DigestValue></Reference></SignedInfo><SignatureValue>DrdlXvWU2+h1Ktt5CTIjTRXwDL/EvCYoD5zorajvmt4m0G8bz1RkNaC5gMBCnbxpD+TXVmVhWFakR++ElAASW6xZhbiXHmRGYBWH7RcNcUFXEg9xUMYDSOW9bkVxaRlkku/E/jSBJ3XzQo2tyn6OKw23MLe2gcN00+uMXA/KXBRFjs2cCu2Zw1eYluETP90dERacfPcC+o9FUyfQR72zw0V9QnuRS3IZ2GK5vakF8vR9j8ZfFVz9i/jYDntX7lwYopWbqqL6whWV60kAE1z45imZzS5hW5lw5rjc+20i3MGOBAwQ5zrp8ZQn5L99p2cNrb1ySWIYb935AUZGVRwICw==</SignatureValue><KeyInfo><KeyValue><RSAKeyValue><Modulus>l64q8AFRN7IxR8oMAlHWCKWWb6w7sL2g/QpBpl7853wMiJ9vqo508vBZaSrvejEM+H9J0tjVaiuM3X4Qkru+Zs3pQDbvFpvXNky0FbPTwYF/P2SvIVt60e22+ZKfYpA92aeIvaoXoFt37pHMnjW1wrWuDGc/W0tQeAn07SmEOENIMP6rucqifSdGg/AujO4+PQM79H5rW1Ba8ArWRED8Vye5CS5hZXH4I15hDujn3RYBR9ylBBVMYP7+rDPGxKNVRrHv8dSkMBp3rninOtKrPVQZrOk8pr3Dnm1dh/8+6tgsrdTHJ3xDKHt3c8ZmmJaMoTYVLPAXufo3NC4TWaKFsw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue></KeyValue><X509Data><X509SubjectName>OID.0.9.2342.19200300.100.1.1=MST:0101360697, CN=CÔNG TY CỔ PHẦN BKAV, L=Cầu Giấy, S=Hà Nội, C=VN</X509SubjectName><X509Certificate>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</X509Certificate></X509Data></KeyInfo></Signature></CKyDTu></DKyThueDTu>";
				//string strXML = @"<?xml version='1.0' encoding='UTF-8'?><S:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:S=""http://schemas.xmlsoap.org/soap/envelope/""><S:Body><ns0:nhanHSoThueResponse xmlns:ns0=""http://nhanhsothue.van.gdt.gov.vn/""><ns0:return>00010-0002-12032019172312277-42528</ns0:return></ns0:nhanHSoThueResponse></S:Body></S:Envelope>";
				//string strXML = @"<TBaoThueDTu xmlns=""http://kekhaithue.gdt.gov.vn/TBaoThue"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">     <TBaoThue id=""_NODE_TO_SIGN"">        <TTinChung>           <CQT>              <maCQT>00000</maCQT>              <tenCQT>Tổng Cục Thuế</tenCQT>              <DVu>                 <maDVu>IHTKK</maDVu>                 <tenDVu>IHTKK</tenDVu>                 <pbanDVu>3.1.0</pbanDVu>              </DVu>           </CQT>           <NNhanTBaoThue>              <maNNhan>0000000000</maNNhan>              <tenNNhan/>              <diaChiNNhan/>           </NNhanTBaoThue>           <TTinTBaoThue>              <maTBao>213</maTBao>              <tenTBao>Giao dịch không hợp lệ</tenTBao>              <pbanTBao>2.0.9</pbanTBao>              <soTBao/>              <ngayTBao>2019-03-09</ngayTBao>           </TTinTBaoThue>        </TTinChung>        <NDungTBao>           <GDich>              <mauLoaiThongBao/>              <tenLoaiThongBao>Giao dịch không hợp lệ</tenLoaiThongBao>              <ngayGDich>09/03/2019</ngayGDich>              <maGDich>00010-0002-0903201910453447-42480</maGDich>              <maLoaiGDich>####</maLoaiGDich>              <ndungGDich>Đăng ký hồ sơ khai thuế nộp qua dịch vụ VAN</ndungGDich>              <maKQuaGDich>03</maKQuaGDich>              <kquaGDich>Không chấp nhận</kquaGDich>              <maLoiGDich>IHTKK-9999</maLoiGDich>              <mtaLoiGDich>Tờ khai không đúng định dạng với XSD: Premature end of file.</mtaLoiGDich>           </GDich>           <CTietGDich>              <maGDC/>           </CTietGDich>        </NDungTBao>     </TBaoThue>  <CKyDTu><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments""/><SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1""/><Reference URI=""#_NODE_TO_SIGN""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature""/></Transforms><DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1""/><DigestValue>Had/Pq/vkvTdbgJskVzozbAzyaM=</DigestValue></Reference></SignedInfo><SignatureValue>Eb1e6+v1BqmYKaQBvnMJoZ0kWfjbDe5Dyp4I753LafsWnQghXp4KLj6CtBteUGse+fX605Doyk3Y  gUJWOPx0siTEJXlf3psmmMoHuwr5aTeOr+Csx1mSw1I6seseOxlCpovkXrZkZaW7ZfvI4FonaR8Y  d6O8AZHyXIYRaFAe/i0TS+JrLsRLu5Pwklvp3Jc9tXB1K1qPFKNKEZgGcrCOK2Fh19Bhlqb4qpbV  O5hmK3pM/RKOnHNwIUmIG0O5fViAUri2/o1mEPILl52F4WDlZcDCafXZ1E9yKt1IBS/oYeeQB/gZ  LUTsM/XzgN/3JlVIh+dpB1ywkSlVLdZ+EQwqcg==</SignatureValue><KeyInfo><KeyValue><RSAKeyValue><Modulus>kKg/XBu1Cex/Q36/m9/9YISS69FpfFsZX9NhPwwjOJG6t7SUqZU5C82TMc2VzcF8w0V6SzvA3evB  j52TqFQG06jhVpfoilFaJaav3OnM80yDnmJnyEVVAA9iSIDbpxAm3dDP945x1hOYxTId7gMYqyL7  29qEAbUgpuFrdFwKHYsQp5TnBq0zZG7nZaQiHbTIUVaCG/fFtdM2IhVFG5+tzeFF6FrzHyd1Du22  AyoD7PrkPsQsc3cEPLjZHtEulQTxiz36+WcVh6OQWZLo+0dBDXwVI9Tb2c/zaHbK7AoQZ2O2iNrm  nsKO0RlEn0s+5Slfe9jEA0kTjXqWom6etqKYhQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue></KeyValue><X509Data><X509SubjectName>C=VN,L=Sô  123 Lò Đúc \, Hai Bà Trưng\, Hà Nội,O=MST:0100231226,CN=Tổng  cục Thuế</X509SubjectName><X509Certificate>MIIFMTCCBBmgAwIBAgIQVAEBBHBaECcrI/rg5a8qUjANBgkqhkiG9w0BAQUFADBuMQswCQYDVQQG  EwJWTjEYMBYGA1UEChMPRlBUIENvcnBvcmF0aW9uMR8wHQYDVQQLExZGUFQgSW5mb3JtYXRpb24g  U3lzdGVtMSQwIgYDVQQDExtGUFQgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkwHhcNMTkwMjIyMTQ1  NjMzWhcNMjAxMDIxMDM0NDU3WjB+MR0wGwYDVQQDDBRU4buVbmcgIGPhu6VjIFRodeG6vzEXMBUG  A1UECgwOTVNUOjAxMDAyMzEyMjYxNzA1BgNVBAcMLlPDtCAgMTIzIEzDsiDEkMO6YyAsIEhhaSBC  w6AgVHLGsG5nLCBIw6AgTuG7mWkxCzAJBgNVBAYTAlZOMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A  MIIBCgKCAQEAkKg/XBu1Cex/Q36/m9/9YISS69FpfFsZX9NhPwwjOJG6t7SUqZU5C82TMc2VzcF8  w0V6SzvA3evBj52TqFQG06jhVpfoilFaJaav3OnM80yDnmJnyEVVAA9iSIDbpxAm3dDP945x1hOY  xTId7gMYqyL729qEAbUgpuFrdFwKHYsQp5TnBq0zZG7nZaQiHbTIUVaCG/fFtdM2IhVFG5+tzeFF  6FrzHyd1Du22AyoD7PrkPsQsc3cEPLjZHtEulQTxiz36+WcVh6OQWZLo+0dBDXwVI9Tb2c/zaHbK  7AoQZ2O2iNrmnsKO0RlEn0s+5Slfe9jEA0kTjXqWom6etqKYhQIDAQABo4IBuTCCAbUwgZ4GCCsG  AQUFBwEBBIGRMIGOMDcGCCsGAQUFBzAChitodHRwOi8vcHVibGljLnJvb3RjYS5nb3Yudm4vY3J0  L21pY25yY2EuY3J0MC8GCCsGAQUFBzAChiNodHRwOi8vd3d3LmZpcy5jb20udm4vY3J0L2ZwdGNh  LmNydDAiBggrBgEFBQcwAYYWaHR0cDovL29jc3AuZmlzLmNvbS52bjAdBgNVHQ4EFgQUM2zXoZGS  W2pAyzyY2kUPQRfwYQAwDAYDVR0TAQH/BAIwADAfBgNVHSMEGDAWgBSz3nGSHEUdyZtP9xeVU86i  TetZMjAZBgNVHSAEEjAQMA4GDCsGAQQBge0DAQQBBjAtBgNVHR8BAf8EIzAhMB+gHaAbhhlodHRw  Oi8vY3JsLmZpcy5jb20udm4vZ2V0MA4GA1UdDwEB/wQEAwIB/jBqBgNVHSUBAf8EYDBeBggrBgEF  BQcDAQYIKwYBBQUHAwIGCCsGAQUFBwMDBggrBgEFBQcDBAYIKwYBBQUHAw8GCCsGAQUFBwMQBggr  BgEFBQcDEQYKKwYBBAGCNxQCAgYKKwYBBAGCNwoDDDANBgkqhkiG9w0BAQUFAAOCAQEAPlnTg4d9  rdl/FaKEFPXAiR+Jwijy8xckQJ17bzxaOkDZxvFSMirzHC3OT5Hd69QyPTK4L9QSNEsgELJIsvDV  KVthqJsco+nwqrqM2v85prMHrOonbGIxPWnW1xqYj9Zprj/QM9chgAZFT3v2fYejiA5kaevAQny3  3/mJrG62uNKlZ50aGB2Y0ew8LsMucHLau56OXn1armm0C0Egqtv8thTFXvUuM+uo7m2A+w7jjG4g  lreG7x//9rDONEwadkamF8z65ysQfkYkKLt2C9/x0ji3SpCml0xG+IWZBFYh8VFrUEShekVDVtCN  Ulaxrgftmjg6AK/tgHfb1GqUVvPvOw==</X509Certificate></X509Data></KeyInfo></Signature></CKyDTu></TBaoThueDTu>";
				//string strXML = @"<ns0:nhanHSoThueResponse xmlns:ns0=""http://nhanhsothue.van.gdt.gov.vn/""><ns0:return>00010-0002-13032019095603605-42541</ns0:return></ns0:nhanHSoThueResponse>";
				//string strXML = @"<ns0:Fault xmlns:ns1=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns0=""http://schemas.xmlsoap.org/soap/envelope/""><faultcode>ns0:Server</faultcode><faultstring>javax.xml.bind.UnmarshalException - with linked exception: Exception [EclipseLink-25004] (Eclipse Persistence Services - 2.4.2.v20130514-5956486): org.eclipse.persistence.exceptions.XMLMarshalException Exception Description: An error occurred unmarshalling the document Internal Exception: javax.xml.ws.WebServiceException: There's no attachment for the content ID ""cid:265578110741""]</faultstring></ns0:Fault>";
				var encoding = new UnicodeEncoding();
				byte[] buffer = System.Text.Encoding.UTF8.GetBytes(strXML);
				string strSoapBody = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

				DataSet ds = CmUtils.XmlUtils.Xml2DataSet(strXML);
			}
			#endregion

			#region // Split String:
			if (bTest)
			{
				string strXML = @"--uuid:901c7bf4-e85e-444f-999e-af9b10c2b9f8  Content-Type: text/xml; charset=utf-8    <?xml version='1.0' encoding='UTF-8'?><S:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:S=""http://schemas.xmlsoap.org/soap/envelope/""><S:Body><ns0:traKQuaGDichResponse xmlns:ns0=""http://kquagdich.van.gdt.gov.vn/""><ns0:return>cid:6bec049a-8c16-4156-9d58-ee1aec101d8c@example.jaxws.sun.com</ns0:return></ns0:traKQuaGDichResponse></S:Body></S:Envelope>  --uuid:901c7bf4-e85e-444f-999e-af9b10c2b9f8  Content-Id:<6bec049a-8c16-4156-9d58-ee1aec101d8c@example.jaxws.sun.com>  Content-Type: application/octet-stream  Content-Transfer-Encoding: binary    <?xml version=""1.0"" encoding=""UTF-8""?><TBaoThueDTu xmlns=""http://kekhaithue.gdt.gov.vn/TBaoThue"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">     <TBaoThue id=""_NODE_TO_SIGN"">        <TTinChung>           <CQT>              <maCQT>00000</maCQT>              <tenCQT>Tổng Cục Thuế</tenCQT>              <DVu>                 <maDVu>IHTKK</maDVu>                 <tenDVu>IHTKK</tenDVu>                 <pbanDVu>3.1.0</pbanDVu>              </DVu>           </CQT>           <NNhanTBaoThue>              <maNNhan>0000000000</maNNhan>              <tenNNhan/>              <diaChiNNhan/>           </NNhanTBaoThue>           <TTinTBaoThue>              <maTBao>213</maTBao>              <tenTBao>Giao dịch không hợp lệ</tenTBao>              <pbanTBao>2.0.9</pbanTBao>              <soTBao/>              <ngayTBao>2019-03-09</ngayTBao>           </TTinTBaoThue>        </TTinChung>        <NDungTBao>           <GDich>              <mauLoaiThongBao/>              <tenLoaiThongBao>Giao dịch không hợp lệ</tenLoaiThongBao>              <ngayGDich>09/03/2019</ngayGDich>              <maGDich>00010-0002-0903201910453447-42480</maGDich>              <maLoaiGDich>####</maLoaiGDich>              <ndungGDich>Đăng ký hồ sơ khai thuế nộp qua dịch vụ VAN</ndungGDich>              <maKQuaGDich>03</maKQuaGDich>              <kquaGDich>Không chấp nhận</kquaGDich>              <maLoiGDich>IHTKK-9999</maLoiGDich>              <mtaLoiGDich>Tờ khai không đúng định dạng với XSD: Premature end of file.</mtaLoiGDich>           </GDich>           <CTietGDich>              <maGDC/>           </CTietGDich>        </NDungTBao>     </TBaoThue>  <CKyDTu><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments""/><SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1""/><Reference URI=""#_NODE_TO_SIGN""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature""/></Transforms><DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1""/><DigestValue>Had/Pq/vkvTdbgJskVzozbAzyaM=</DigestValue></Reference></SignedInfo><SignatureValue>Eb1e6+v1BqmYKaQBvnMJoZ0kWfjbDe5Dyp4I753LafsWnQghXp4KLj6CtBteUGse+fX605Doyk3Y  gUJWOPx0siTEJXlf3psmmMoHuwr5aTeOr+Csx1mSw1I6seseOxlCpovkXrZkZaW7ZfvI4FonaR8Y  d6O8AZHyXIYRaFAe/i0TS+JrLsRLu5Pwklvp3Jc9tXB1K1qPFKNKEZgGcrCOK2Fh19Bhlqb4qpbV  O5hmK3pM/RKOnHNwIUmIG0O5fViAUri2/o1mEPILl52F4WDlZcDCafXZ1E9yKt1IBS/oYeeQB/gZ  LUTsM/XzgN/3JlVIh+dpB1ywkSlVLdZ+EQwqcg==</SignatureValue><KeyInfo><KeyValue><RSAKeyValue><Modulus>kKg/XBu1Cex/Q36/m9/9YISS69FpfFsZX9NhPwwjOJG6t7SUqZU5C82TMc2VzcF8w0V6SzvA3evB  j52TqFQG06jhVpfoilFaJaav3OnM80yDnmJnyEVVAA9iSIDbpxAm3dDP945x1hOYxTId7gMYqyL7  29qEAbUgpuFrdFwKHYsQp5TnBq0zZG7nZaQiHbTIUVaCG/fFtdM2IhVFG5+tzeFF6FrzHyd1Du22  AyoD7PrkPsQsc3cEPLjZHtEulQTxiz36+WcVh6OQWZLo+0dBDXwVI9Tb2c/zaHbK7AoQZ2O2iNrm  nsKO0RlEn0s+5Slfe9jEA0kTjXqWom6etqKYhQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue></KeyValue><X509Data><X509SubjectName>C=VN,L=Sô  123 Lò Đúc \, Hai Bà Trưng\, Hà Nội,O=MST:0100231226,CN=Tổng  cục Thuế</X509SubjectName><X509Certificate>MIIFMTCCBBmgAwIBAgIQVAEBBHBaECcrI/rg5a8qUjANBgkqhkiG9w0BAQUFADBuMQswCQYDVQQG  EwJWTjEYMBYGA1UEChMPRlBUIENvcnBvcmF0aW9uMR8wHQYDVQQLExZGUFQgSW5mb3JtYXRpb24g  U3lzdGVtMSQwIgYDVQQDExtGUFQgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkwHhcNMTkwMjIyMTQ1  NjMzWhcNMjAxMDIxMDM0NDU3WjB+MR0wGwYDVQQDDBRU4buVbmcgIGPhu6VjIFRodeG6vzEXMBUG  A1UECgwOTVNUOjAxMDAyMzEyMjYxNzA1BgNVBAcMLlPDtCAgMTIzIEzDsiDEkMO6YyAsIEhhaSBC  w6AgVHLGsG5nLCBIw6AgTuG7mWkxCzAJBgNVBAYTAlZOMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A  MIIBCgKCAQEAkKg/XBu1Cex/Q36/m9/9YISS69FpfFsZX9NhPwwjOJG6t7SUqZU5C82TMc2VzcF8  w0V6SzvA3evBj52TqFQG06jhVpfoilFaJaav3OnM80yDnmJnyEVVAA9iSIDbpxAm3dDP945x1hOY  xTId7gMYqyL729qEAbUgpuFrdFwKHYsQp5TnBq0zZG7nZaQiHbTIUVaCG/fFtdM2IhVFG5+tzeFF  6FrzHyd1Du22AyoD7PrkPsQsc3cEPLjZHtEulQTxiz36+WcVh6OQWZLo+0dBDXwVI9Tb2c/zaHbK  7AoQZ2O2iNrmnsKO0RlEn0s+5Slfe9jEA0kTjXqWom6etqKYhQIDAQABo4IBuTCCAbUwgZ4GCCsG  AQUFBwEBBIGRMIGOMDcGCCsGAQUFBzAChitodHRwOi8vcHVibGljLnJvb3RjYS5nb3Yudm4vY3J0  L21pY25yY2EuY3J0MC8GCCsGAQUFBzAChiNodHRwOi8vd3d3LmZpcy5jb20udm4vY3J0L2ZwdGNh  LmNydDAiBggrBgEFBQcwAYYWaHR0cDovL29jc3AuZmlzLmNvbS52bjAdBgNVHQ4EFgQUM2zXoZGS  W2pAyzyY2kUPQRfwYQAwDAYDVR0TAQH/BAIwADAfBgNVHSMEGDAWgBSz3nGSHEUdyZtP9xeVU86i  TetZMjAZBgNVHSAEEjAQMA4GDCsGAQQBge0DAQQBBjAtBgNVHR8BAf8EIzAhMB+gHaAbhhlodHRw  Oi8vY3JsLmZpcy5jb20udm4vZ2V0MA4GA1UdDwEB/wQEAwIB/jBqBgNVHSUBAf8EYDBeBggrBgEF  BQcDAQYIKwYBBQUHAwIGCCsGAQUFBwMDBggrBgEFBQcDBAYIKwYBBQUHAw8GCCsGAQUFBwMQBggr  BgEFBQcDEQYKKwYBBAGCNxQCAgYKKwYBBAGCNwoDDDANBgkqhkiG9w0BAQUFAAOCAQEAPlnTg4d9  rdl/FaKEFPXAiR+Jwijy8xckQJ17bzxaOkDZxvFSMirzHC3OT5Hd69QyPTK4L9QSNEsgELJIsvDV  KVthqJsco+nwqrqM2v85prMHrOonbGIxPWnW1xqYj9Zprj/QM9chgAZFT3v2fYejiA5kaevAQny3  3/mJrG62uNKlZ50aGB2Y0ew8LsMucHLau56OXn1armm0C0Egqtv8thTFXvUuM+uo7m2A+w7jjG4g  lreG7x//9rDONEwadkamF8z65ysQfkYkKLt2C9/x0ji3SpCml0xG+IWZBFYh8VFrUEShekVDVtCN  Ulaxrgftmjg6AK/tgHfb1GqUVvPvOw==</X509Certificate></X509Data></KeyInfo></Signature></CKyDTu></TBaoThueDTu>  --uuid:901c7bf4-e85e-444f-999e-af9b10c2b9f8--";
				int Pos1 = strXML.IndexOf("<TBaoThueDTu xmlns");
				int Pos2 = strXML.IndexOf("</TBaoThueDTu>") + ("</TBaoThueDTu>").Length;
				string strFinalString = strXML.Substring(Pos1, Pos2 - Pos1);

				string strResult = strFinalString;

				DataSet ds = CmUtils.XmlUtils.Xml2DataSet(strResult);
			}
			#endregion
		}
		#endregion

		#region // Test UTC:
		private void Test_UTC()
		{
			bool bTest = false;

			if (bTest)
			{
				DateTime dtimeSys = DateTime.Now;

				string strdtimeSys = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				string strdtimeSysUTC = dtimeSys.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

				DateTime dateTimeUTC = TUtils.CUtils.ConvertingLocalTimeToUTC(dtimeSys);

				string createdStr = dateTimeUTC.ToString("yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.InvariantCulture);
			}
		}
		#endregion

		#region // HttpWebRequest:

		#endregion

		#region // MyEnum:
		public List<NameValue> EnumToList<T>()
		{
			var array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());
			var array2 = Enum.GetNames(typeof(T)).ToArray<string>();
			List<NameValue> lst = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (lst == null)
					lst = new List<NameValue>();
				string name = array2[i];
				T value = array[i];
				lst.Add(new NameValue { Name = name, Value = value });
			}
			return lst;
		}
		public class NameValue
		{
			public string Name { get; set; }
			public object Value { get; set; }
		}

		#endregion

		private void btnMyTest_Click(object sender, EventArgs e)
		{
            try
            {
                MyTestMix_01_GenNetwork();
            }
            catch (Exception exc)
            {
                CommonForms.Utils.ProcessExc(exc);
            }
        }

		#region // MyTest:
		private void MyTestMix_01_GenNetwork()
		{
			////
			bool bTest = false;
			int nSeq = 0;
			string strTid = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nSeq++);
			DateTime dtimeSys = DateTime.Now;
			DataTable dtDB_MQ_Mst_Network = null;
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			string strErrorCodeDefault = "MyTestMix_01_GenNetwork";
            string strFunctionName = "MyTestMix_01_GenNetwork";

            ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", "MyTestMix_01_GenNetwork"
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});

			System.Collections.Specialized.NameValueCollection nvcParams = System.Configuration.ConfigurationManager.AppSettings;
			string strConnStr_HDDT_RptSv = nvcParams["Biz_DBConnStr_HDDT_RptSv"];
			EzDAL.MyDB.IEzDAL db_RptSv_HDDT = new EzDAL.MyDB.EzDALSqlSv(strConnStr_HDDT_RptSv);

			// Log:
			TLog.Core.CLog log = new TLog.Core.CLog(
				nvcParams["Biz_DBConnStr_HDDT_RptSv_TLog"] // strConnStr
				, nvcParams["TLog_AccountList"] // strAccountList
				, Convert.ToInt32(nvcParams["TLog_DelayForLazyMS"]) // nDelayForLazy
				);

			log.StartBackGroundProcess();

			#region // MQ_Mst_Network:
			if (!bTest)
			{
				// Test1:
				//string strConnStr = nvcParams["Biz_DBConnStr"];
				////DataTable dt_t10 = null;
				////int nResult = 0;
				//EzDAL.MyDB.IEzDAL db = new EzDAL.MyDB.EzDALSqlSv(strConnStr);

				db_RptSv_HDDT.BeginTransaction();
				
				try
				{
					string strSqlGetDB_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
							---- MQ_Mst_Network:
							select top 1
								t.NetworkID 
								, t.MST
							from MQ_Mst_Network --//[mylock]
							where (1=1)
								and t.FlagActive = '0'
							order by 
								t.AutoId
							;
						");

					dtDB_MQ_Mst_Network = db_RptSv_HDDT.ExecQuery(strSqlGetDB_MQ_Mst_Network).Tables[0];

				}
				catch (Exception exc)
				{
					db_RptSv_HDDT.Rollback();

					// Return Bad:
					TUtils.CProcessExc.Process(
						ref mdsFinal
						, exc
						, strErrorCodeDefault
						, alParamsCoupleError.ToArray()
						);
				}
				finally
				{
                    //// Init:
                    //object[] arrobjParamsCouple = null;
                    //object strAppLogType3 = null;
                    //if (string.Equals(CmUtils.CMyDataSet.GetErrorCode(mdsFinal), TError.Error.NoError))
                    //{
                    //    strAppLogType3 = (CmUtils.CMyDataSet.HasWarning(mdsFinal) ? "WARNING" : null);
                    //    arrobjParamsCouple = CmUtils.CMyDataSet.GetWarningParams(mdsFinal);
                    //}
                    //else
                    //{
                    //    strAppLogType3 = null;
                    //    arrobjParamsCouple = CmUtils.CMyDataSet.GetErrorParams(mdsFinal);
                    //}

                    log.WriteLog(
                        nvcParams["Biz_Name"] // strGwUserCode
                        , nvcParams["Biz_LogPw"] // strGwPassword
                        , "1" // strFlagDelayForLazy
                        , strTid // strAppTid
                        , "SYS" // strAppRootSvCode
                        , "SYS" // strAppRootUserCode
                        , "SYS" // strAppServiceCode
                        , "SYS" // strAppUserCode
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") // strAppTDateTime
                        , CmUtils.CMyDataSet.GetErrorCode(mdsFinal) // strAppErrorCode
                        , strFunctionName // strAppLogType1
                        , "RS" // strAppLogType2 = Result
                        , "strAppLogType3" // strAppLogType3
                        , "VN" // strAppLanguageCode
                        , CmUtils.CMyDataSet.GetRemark(mdsFinal) // strAppRemark
                        , alParamsCoupleError.ToArray() // arrobjParamsCouple
                        );
                }

				// Pause:
				System.Threading.Thread.Sleep(100);
			}

			#endregion



		}
		#endregion


	}
}