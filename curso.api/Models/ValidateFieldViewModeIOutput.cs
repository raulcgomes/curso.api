using System.Collections.Generic;

namespace curso.api.Models
{
    public class ValidateFieldViewModeIOutput
    {
        public IEnumerable<string> Errors { get; private set; }

        public ValidateFieldViewModeIOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
