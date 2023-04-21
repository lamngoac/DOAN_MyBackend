using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    // Client
    public class ClientException : Exception
    {
        public c_K_DT_Sys c_K_DT_Sys { get; set; }
        public Exception Exception { get; set; }

        public ClientException()
        {

        }

        public ClientException(c_K_DT_Sys cKDTSys, Exception exception)
        {
            this.c_K_DT_Sys = new c_K_DT_Sys()
            {
                Lst_c_K_DT_SysInfo = new List<c_K_DT_SysInfo>(),
                Lst_c_K_DT_SysError = new List<c_K_DT_SysError>(),
                Lst_c_K_DT_SysWarning = new List<c_K_DT_SysWarning>(),
            };

            this.Exception = new Exception();

            if(cKDTSys != null)
            {
                if(cKDTSys.Lst_c_K_DT_SysInfo != null && cKDTSys.Lst_c_K_DT_SysInfo.Count > 0)
                {
                    this.c_K_DT_Sys.Lst_c_K_DT_SysInfo.AddRange(cKDTSys.Lst_c_K_DT_SysInfo);
                }

                if (cKDTSys.Lst_c_K_DT_SysError != null && cKDTSys.Lst_c_K_DT_SysError.Count > 0)
                {
                    this.c_K_DT_Sys.Lst_c_K_DT_SysError.AddRange(cKDTSys.Lst_c_K_DT_SysError);
                }

                if (cKDTSys.Lst_c_K_DT_SysWarning != null && cKDTSys.Lst_c_K_DT_SysWarning.Count > 0)
                {
                    this.c_K_DT_Sys.Lst_c_K_DT_SysWarning.AddRange(cKDTSys.Lst_c_K_DT_SysWarning);
                }
            }

            if(exception != null)
            {
                this.Exception = exception;
            }
        }
    }
}
