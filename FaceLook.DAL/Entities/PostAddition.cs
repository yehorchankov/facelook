#region

using System.Web.Mvc;

#endregion

namespace FaceLook.DAL.Entities
{
    public class PostAddition
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public byte[] Data { get; set; }
        public string MimeType { get; set; }
        public string Information { get; set; }
    }
}