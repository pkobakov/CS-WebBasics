namespace SUS.MvcFramework
{
    using SUS.HTTP;
    using System;

    public abstract class BaseHttpAttribute : Attribute
    {
        public string Url { get; set; }
        public abstract HttpMethod Method { get;}
    }
}
