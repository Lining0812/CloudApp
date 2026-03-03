using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Interfaces
{
    public interface IFileContent
    {
        string FileName { get; }
        string ContentType { get; }
        long Length { get; }
        Stream OpenReadStream();
        void CopyTo(Stream target);
    }
}
