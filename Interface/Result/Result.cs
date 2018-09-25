namespace Interface.Result
{
    public class Result<T> : IResult<T>
    {
        public bool IsSuccess { get; set; }

        public ResultTypeEnum ResultType { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Result()
        {
            Data = default(T);
        }

        public Result(T Data)
            : this(true, ResultTypeEnum.None, string.Empty, Data, false, string.Empty, string.Empty)
        {
        }
        public Result(T Data, int DataCount)
           : this(true, ResultTypeEnum.None, string.Empty, Data, false, string.Empty, string.Empty, DataCount)
        {
        }
        public Result(ResultTypeEnum ResultTypeEnum, T Data, string Message, string SummaryMessage)
          : this(true, ResultTypeEnum, string.Empty, Data, false, Message, SummaryMessage)
        {
        }
        public Result(ResultTypeEnum ResultTypeEnum, T Data, string Message, string SummaryMessage, int DataCount)
          : this(true, ResultTypeEnum, string.Empty, Data, false, Message, SummaryMessage, DataCount)
        {
        }

        public Result(bool IsSuccess, string Message, string SummaryMessage)
          : this(IsSuccess, ResultTypeEnum.None, string.Empty, default(T), false, Message, SummaryMessage)
        {
        }
        public Result(bool IsSuccess, ResultTypeEnum ResultType, string Message, string SummaryMessage)
            : this(IsSuccess, ResultType, string.Empty, default(T), false, Message, SummaryMessage)
        {
        }

        public Result(bool IsSuccess, ResultTypeEnum ResultType, string Html, T Data, bool IsLastPackage, string Message, string SummaryMessage, int DataCount = 0)
        {
            this.IsSuccess = IsSuccess;
            this.ResultType = ResultType;
            this.Message = Message;
            this.Data = Data;
        }
    }
}
