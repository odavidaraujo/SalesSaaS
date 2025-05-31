using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSaaS.Domain.Entities {
    public class Company {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;

        // Relacionamento com usuário dono
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
