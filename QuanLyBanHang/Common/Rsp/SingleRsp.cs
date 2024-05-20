using System;
using System.Collections.Generic;
using System.Text;

namespace QLBH.Common.Rsp
{
    public class SingleRsp : BaseRsp
    {
        public object Resutls { get; set; }
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public SingleRsp() : base() { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        public SingleRsp(string message) : base(message) { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="titleError">Title error</param>
        public SingleRsp(string message, string titleError) : base(message, titleError) { }

        /// <summary>
        /// Set data
        /// </summary>
        /// <param name="code">Success code</param>
        /// <param name="data">Data</param>
        public void SetData(string code, object data)
        {
            Code = code;
            Resutls = data;
        }
        public void SetDatas(string statusCode, object data)
        {
            Resutls = new { StatusCode = statusCode, Data = data };
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Data
        /// </summary>


        #endregion
    }
}