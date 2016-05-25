using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Exceptions;
using System.Reflection;
namespace TwaijaComposite.Modules.ColumnsManager.Column
{
   public class ColumnResolutionServiceImp:IColumnResolutionService
    {
        private readonly IDirector director;
        private Dictionary<string, IColumnPartsFactory> PartsFactories { get; set; }
        private IEnumerable<IColumnPartsFactory> _factories;
        private IColumnSkeletonFactory _skeletonFactory;
        private Preferences _pref;
        public ColumnResolutionServiceImp(IDirector director,IColumnSkeletonFactory skeletonFactory,IEnumerable<IColumnPartsFactory> factories,Preferences pref)
        {
            _skeletonFactory = skeletonFactory;
            _factories = factories;
            this.director = director;
            _pref = pref;
        }
        IUser ResolveCorrectUserType(Type expectedType)
        {
            if (!(_pref.TransparentUsersFacade.Userrepository.SelectedUser.GetType().IsAssignableFrom(expectedType)))
            {
                foreach (IUser user in _pref.TransparentUsersFacade.Userrepository.Users)
                {
                    if (user.GetType().IsAssignableFrom(expectedType))
                    {
                        return user;
                    }
                }
            }
            return null;
        }
        public void Initialize()
        {
            try
            {
                PartsFactories = new Dictionary<string, IColumnPartsFactory>();
                foreach (IColumnPartsFactory factory in _factories)
                {
                    PartsFactories.Add(factory.FactoryName, factory);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Initialization of ColumnResolutionStrategy Failed:reason:" + e.Message, e);
            }
        }
        private IColumnPartsFactory ResolveIColumnPartsFactory(string name)
        {
            return PartsFactories[name];
        }
        private IColumn ResolveIColumnImp(string name)
        {
            return _skeletonFactory.Create(name);
        }
        private IColumnBuilder ResolveIColumnBuilder(Dictionary<string,object> args, IColumn columnimp, IColumnPartsFactory factory,IUser user,string ColumnBuildType)
        {
            IColumnBuilder builder = null;
            if (string.IsNullOrEmpty(ColumnBuildType))
            {
                builder = new ColumnBuilderBaseImp(columnimp, user, factory,args);
            }
            else
            {
                //Switch ColumnBuilderTypes based on the name supplied by the command.
                //switch (args.ColumnBuildType)
                //{
                //    case "blablabla":

                //        break;
                //    case "":
                //        break;
                //}
            }
            return builder;
        }
        public IHeaderAndContentObject HandleEvent(Common.Events.CreateColumnEventArgs args)
        {
            IColumnPartsFactory factory = null;
            IColumn columnimp = null; 
            IColumnBuilder builder = null;
            columnimp = ResolveIColumnImp(args.ColumnImpType);
            factory = ResolveIColumnPartsFactory(args.ColumnType);
            if (columnimp != null && factory != null)
            {
                IUser user = null;
                args.User = (args.User != null) ? args.User : _pref.TransparentUsersFacade.Userrepository.SelectedUser;
               
                builder = ResolveIColumnBuilder(args.Parameters, columnimp, factory, args.User, args.ColumnBuildType);
                try
                {
                    director.Construct(builder);
                }
                catch (WrongUserTypeException e)
                {
                    try
                    {
                        var correct = ResolveCorrectUserType(e.ExpectedType);
                        if (correct != null)
                        {
                            builder = ResolveIColumnBuilder(args.Parameters, columnimp, factory, correct, args.ColumnBuildType);
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("There is no User that can fulfill your request");
                    }
                }
                catch (ArgumentNullException g)
                {

                }
                return builder.GetProduct();        
            }
            return null;
        }

        public T HandleEvent<T>(CreateColumnEventArgs args) where T : class,IHeaderAndContentObject
        {
            try
            {
                return HandleEvent(args) as T;
            }
            catch
            {
                return null;
            }

        }
    }
}
