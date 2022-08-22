using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();
        }
        public ErrorDto(string error, bool isShow)
        {
            (Errors = new List<string>()).Add(error);
            IsShow = isShow;
        }
        public ErrorDto(List<string> errors, bool isShow)
        {
            (Errors = new List<string>()).AddRange(errors);
            IsShow = isShow;
        }
        public List<string> Errors { get; private set; }
        public bool IsShow { get; private set; }
    }
}
