namespace Application.DTOs.Response
{
    public class ObjectResponse<T>
    {

        public StatusCodeObjectResponse Status { get; set; } = StatusCodeObjectResponse.Sucess;
        public List<string> Message { get; set; } = new List<string>();
        public T? Data { get; set; }

        //preciso deixar mais simples a forma de como eu crio os erros nos UCs

        public ObjectResponse<T> ReturnSucess(T? data)
        {
            this.Status = StatusCodeObjectResponse.Sucess;
            this.Data = data;
            return this;
        }


        public ObjectResponse<T> ReturnError(T? data)
        {
            this.Status = StatusCodeObjectResponse.Error;
            this.Data = data;
            return this;
        }

        public ObjectResponse<T> ReturnNotFound(T? data)
        {
            this.Status = StatusCodeObjectResponse.NotFound;
            this.Data = data;
            return this;
        }

        public void AddError(string msg)
        {
            this.Message.Add(msg);
        }
        public void AddErrors(List<string> msg)
        {
            this.Message = msg;
        }


    }



    public enum StatusCodeObjectResponse
    {
        Sucess = 200,
        Error = 500,
        NotFound = 401
    }

}
