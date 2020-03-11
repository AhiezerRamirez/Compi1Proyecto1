using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi1Proyecto1
{
    class Estado
    {
        public int id;
        public ArrayList transiciones = new ArrayList();
        public Estado(int id,ArrayList transiciones)
        {
            this.id = id;
            this.transiciones = transiciones;
        }

        public Estado(int id)
        {
            this.id = id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        public void setTransiciones(Transicion tran)
        {
            this.transiciones.Add(tran);
        }

        public String toString()
        {
            return this.id.ToString();
        }
    }
}
