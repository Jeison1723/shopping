using Microsoft.AspNetCore.Identity;
using Shopping.Controllers.Data.TipoUsuario;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Controllers.Data.Entities
{
    public class User : IdentityUser
    {
        
            [Display(Name = "Documento")]

            [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]

            [Required(ErrorMessage = "El campo {0} es obligatorio.")]

            public string Document { get; set; }


            [Display(Name = "Nombres")]

            [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]

            [Required(ErrorMessage = "El campo {0} es obligatorio.")]

            public string FirstName { get; set; }


            [Display(Name = "Apellidos")]

            [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]

            [Required(ErrorMessage = "El campo {0} es obligatorio.")]

            public string LastName { get; set; }


        [Display(Name = "Dirección")]

            [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]

            [Required(ErrorMessage = "El campo {0} es obligatorio.")]

            public string Address { get; set; }


            [Display(Name = "Foto")]

            public Guid ImageId { get; set; }


            //TODO: Pending to put the correct paths

            [Display(Name = "Foto")]

            public string ImageFullPath => ImageId == Guid.Empty

                ? $"https://localhost:7172/Images/noimage.jpg"

                : $"https://shoppingzulu.blob.core.windows.net/users/{ImageId}";


            [Display(Name = "Tipo de usuario")]

            public UserType UserType { get; set; }

            [Display(Name = "Ciudad")]

            public City City { get; set; }



            [Display(Name = "Usuario")]

            public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        
    }
}
