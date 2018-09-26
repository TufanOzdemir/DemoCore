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
            : this(true, ResultTypeEnum.None, string.Empty, Data)
        {
        }

        public Result(bool IsSuccess, ResultTypeEnum ResultType, string Message)
            : this(IsSuccess, ResultType, Message, default(T))
        {
        }

        public Result(bool IsSuccess, ResultTypeEnum ResultType, string Message, T Data)
        {
            this.IsSuccess = IsSuccess;
            this.ResultType = ResultType;
            this.Message = Message;
            this.Data = Data;
        }
    }
}
