namespace $safeprojectname$.Entities
{
    public class Entity : DynamicObject, IXmlSerializable, IDictionary<string, object>
    {
        // Private member variables for root name and expando dictionary
        private readonly string _root = "Entity";
        private readonly IDictionary<string, object> _expando = null;

        // Constructor initializes expando dictionary
        public Entity()
        {
            _expando = new ExpandoObject();
        }

        // Override TryGetMember to try getting member value from expando dictionary
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_expando.TryGetValue(binder.Name, out object value))
            {
                result = value;
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        // Override TrySetMember to try setting member value in expando dictionary
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _expando[binder.Name] = value;

            return true;
        }

        // Implement GetSchema method for IXmlSerializable interface (not implemented)
        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        // Implement ReadXml method for IXmlSerializable interface
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(_root);

            while (!reader.Name.Equals(_root))
            {
                string typeContent;
                Type underlyingType;
                var name = reader.Name;

                reader.MoveToAttribute("type");
                typeContent = reader.ReadContentAsString();
                underlyingType = Type.GetType(typeContent);
                reader.MoveToContent();
                _expando[name] = reader.ReadElementContentAs(underlyingType, null);
            }
        }

        // Implement WriteXml method for IXmlSerializable interface
        public void WriteXml(XmlWriter writer)
        {
            foreach (var key in _expando.Keys)
            {
                var value = _expando[key];
                WriteLinksToXml(key, value, writer);
            }
        }

        // Helper method to write links to XML
        private void WriteLinksToXml(string key, object value, XmlWriter writer)
        {
            writer.WriteStartElement(key);
            writer.WriteString(value.ToString());
            writer.WriteEndElement();
        }

        // Implement Add method for IDictionary interface
        public void Add(string key, object value)
        {
            _expando.Add(key, value);
        }

        // Implement ContainsKey method for IDictionary interface
        public bool ContainsKey(string key)
        {
            return _expando.ContainsKey(key);
        }

        // Implement Keys property for IDictionary interface
        public ICollection<string> Keys
        {
            get { return _expando.Keys; }
        }

        // Implement Remove method for IDictionary interface
        public bool Remove(string key)
        {
            return _expando.Remove(key);
        }

        // Implement TryGetValue method for IDictionary interface
        public bool TryGetValue(string key, out object value)
        {
            return _expando.TryGetValue(key, out value);
        }

        // Implement Values property for IDictionary interface
        public ICollection<object> Values
        {
            get { return _expando.Values; }
        }

        // Implement indexer for IDictionary interface
        public object this[string key]
        {
            get
            {
                return _expando[key];
            }
            set
            {
                _expando[key] = value;
            }
        }

        // Implement Add method for ICollection<KeyValuePair<string, object>> interface
        public void Add(KeyValuePair<string, object> item)
        {
            _expando.Add(item);
        }

        // Implement Clear method for ICollection<KeyValuePair<string, object>> interface
        public void Clear()
        {
            _expando.Clear();
        }

        // Implement Contains method for ICollection<KeyValuePair<string, object>> interface
        public bool Contains(KeyValuePair<string, object> item)
        {
            return _expando.Contains(item);
        }

        // Implement CopyTo method for ICollection<KeyValuePair<string, object>> interface
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _expando.CopyTo(array, arrayIndex);
        }

        // Implement Count property for ICollection<KeyValuePair<string, object>> interface
        public int Count
        {
            get { return _expando.Count; }
        }

        // Implement IsReadOnly property for ICollection<KeyValuePair<string, object>> interface
        public bool IsReadOnly
        {
            get { return _expando.IsReadOnly; }
        }

        // Implement Remove method for ICollection<KeyValuePair<string, object>> interface
        public bool Remove(KeyValuePair<string, object> item)
        {
            return _expando.Remove(item);
        }

        // Implement GetEnumerator method for IEnumerable<KeyValuePair<string, object>> interface
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _expando.GetEnumerator();
        }

        // Implement GetEnumerator method for IEnumerable interface
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}