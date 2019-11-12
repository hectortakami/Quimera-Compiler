/*
  Chimera compiler - Program driver.
  Created by Hector Takami & Ernesto Cervantes
  Copyright (C) 2013 Ariel Ortiz, ITESM CEM
  
  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
  
  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.
  
  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Text;
using System.Collections.Generic;

namespace Chimera
{

    public class SymbolTable : IEnumerable<KeyValuePair<string, SymbolTable.Row>>
    {

        public class Row
        {
            public Row(Type type, ProcedureType kind, int pos = -1)
            {
                this.type = type;
                this.kind = kind;
                this.pos = pos;
            }
            public Type type { get; private set; }
            public ProcedureType kind { get; private set; }
            public int pos { get; private set; }

            public override string ToString()
            {
                var posString = pos == -1 ? "-" : $"{pos}";
                string result = "";
                if (type.ToString().Length<=6){
                    result += $"{type}\t\t";
                }else{
                    result += $"{type}\t";
                }
                result += $"{kind}\t\t{posString} |";
                
                //return $"{type},{kind},{posString}";
                return result;
            }
        }

        IDictionary<string, SymbolTable.Row> data = new SortedDictionary<string, SymbolTable.Row>();

        //-----------------------------------------------------------
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("\n===================================================\n");
            sb.Append("|\t\t     SYMBOL TABLE \t\t  |\n");
            sb.Append("===================================================\n");
            sb.Append("| var_name     | var_type     | decl_type | value |\n");
            sb.Append("===================================================\n");
            foreach (var entry in data)
            {
                int length = entry.Key.ToString().Length;
                if (length <=5)
                {
                    sb.Append(String.Format("| {0}\t\t{1}\n",
                                        entry.Key,
                                        entry.Value));
                }
                else
                {
                    sb.Append(String.Format("| {0}\t{1}\n",
                                        entry.Key,
                                        entry.Value));
                }

            }
            sb.Append("===================================================\n");
            return sb.ToString();
        }

        //-----------------------------------------------------------
        public SymbolTable.Row this[string key]
        {
            get
            {
                return data[key];
            }
            set
            {
                data[key] = value;
            }
        }

        //-----------------------------------------------------------
        public bool Contains(string key)
        {
            return data.ContainsKey(key);
        }

        //-----------------------------------------------------------
        public IEnumerator<KeyValuePair<string, SymbolTable.Row>> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        //-----------------------------------------------------------
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
