using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    /// <summary>
    /// Api执行结果模型
    /// </summary>
    public class ResultDto
    {
        /// <summary>
        /// Api执行结果代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Api执行结果描述
        /// </summary>
        public string Message { get; set; }
        public ResultDto()
        {
            Code = 0;
        }
        public static ResultDto Success()
        {
            return new ResultDto() { Message = "ok" };
        }
        public static ResultDto Error(string message, int code = -1)
        {
            return new ResultDto
            {
                Code = code,
                Message = message
            };
        }
    }
    /// <summary>
    /// Api执行结果模型（带有数据）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultDto<T> : ResultDto
    {
        /// <summary>
        /// Api返回数据
        /// </summary>
        public T Data { get; set; }
        public static implicit operator ResultDto<T>(T data)
        {
            return new ResultDto<T>
            {
                Data = data,
                Message = "ok"
            };
        }
        public static ResultDto<T> Success(T data)
        {
            return new ResultDto<T>
            {
                Data = data,
                Message = "ok"
            };
        }        
    }
}
