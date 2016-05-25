using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.ViewModels;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Collections;

namespace TwaijaComposite.Modules.ProfileViewer.Viewmodels
{
    public class ProfileViewmodel:ViewModelBase
    {
        public ProfileViewmodel()
        {
            _secondarycontent = new List<IHeaderAndContentObject>();
        }
        private IHeaderAndContentObject _maincontent;
        public IHeaderAndContentObject MainContent
        {
            get { return _maincontent; }
            set { _maincontent = value;
            value.Initialize();
            }          
        }
        private List<IHeaderAndContentObject> _secondarycontent;
        public IList<IHeaderAndContentObject> SecondaryContent
        {
            get
            {
                return _secondarycontent;
            }
        }
        private IHeaderAndContentObject _selected;
        public IHeaderAndContentObject Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged("Selected");
                    _selected.Initialize();
                }
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            MainContent.Dispose();
            foreach (IHeaderAndContentObject disposable in SecondaryContent)
            {
                disposable.Dispose();
            }

        }
    }
}
