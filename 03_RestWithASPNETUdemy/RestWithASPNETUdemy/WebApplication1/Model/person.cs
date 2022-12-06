using System.Runtime.InteropServices;

// Nos projetos no estilo SDK como este, vários atributos de assembly que sempre eram
// definidos nesse arquivo agora são adicionados automaticamente durante o build e
// populados com os valores definidos nas propriedades do projeto. Para obter detalhes
// de quais atributos são incluídos e como personalizar esse processo, confira: https://aka.ms/assembly-info-properties


// A definição de ComVisible como false torna os tipos neste assembly invisíveis para
// os componentes do COM. Se for necessário acessar um tipo do COM neste assembly,
// defina o atributo ComVisible como true nesse tipo.

[assembly: ComVisible(false)]

// O GUID a seguir será destinado à ID de typelib se este projeto for exposto ao COM.

[assembly: Guid("a2652a69-0133-47ea-86c5-9085eb61e989")]

namespace RestWithASPNETUdemy.Model 
{
    public class Person
    { 
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

    }
}
