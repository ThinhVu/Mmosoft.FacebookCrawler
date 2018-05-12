using System.Collections.Generic;
using FbFarm.Sdk.Models.User;

namespace FbFarm.Sdk.Models.Group
{
    public class GroupInfo
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private List<string> _members;
        public List<string> Members
        {
            get { return _members; }
            set { _members = value; }
        }
        private List<string> _admins;
        public List<string> Admins
        {
            get { return _admins; }
            set { _admins = value; }
        }
    }
}
