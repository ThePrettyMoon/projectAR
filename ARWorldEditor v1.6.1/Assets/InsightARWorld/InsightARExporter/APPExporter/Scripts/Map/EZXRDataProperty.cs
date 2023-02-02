


	namespace ARWorldEditor
    {
        public abstract class EZXRDataProperty
        {
            public event System.EventHandler PropertyHasChanged;
            protected virtual void OnPropertyHasChanged(System.EventArgs e)
            {
                System.EventHandler handler = PropertyHasChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            public virtual bool HasChanged
            {
                set
                {
                    if (value == true)
                    {
                        OnPropertyHasChanged(null /*Pass args here */);
                    }
                }
            }
            public virtual bool NeedsForceUpdate()
            {
                return false;
            }

        }
    }

