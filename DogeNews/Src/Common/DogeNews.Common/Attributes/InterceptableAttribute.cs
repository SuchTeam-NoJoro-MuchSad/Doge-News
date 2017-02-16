using System;
using System.Collections.Generic;
using System.Linq;

namespace DogeNews.Common.Attributes
{
    public class InterceptableAttribute : Attribute
    {
        private IEnumerable<Type> typeOfInterceptors;

        /// <summary>
        /// Adds any count of interceptors to a class to be intercepted
        /// </summary>
        /// <param name="interceptors">[Not null] Note that the order of interceptors in this 
        /// attribute is the order that the interceptors will be executed.</param>
        public InterceptableAttribute(params Type[] interceptors)
        {
            TypeOfInterceptors = interceptors;
        }

        public IEnumerable<Type> TypeOfInterceptors
        {
            get { return typeOfInterceptors; }
            private set
            {
                if (!value.Any())
                {
                    throw new ArgumentException("Interceptable attribute must have at lease one parameter.");
                }

                this.typeOfInterceptors = value;
            }
        }
    }
}