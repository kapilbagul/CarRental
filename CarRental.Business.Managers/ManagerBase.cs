using Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.Serialization;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using System.Threading;
using CarRental.Common;
using Core.Common.Exceptions;

namespace CarRental.Business.Managers
{
    public class ManagerBase
    {
        public ManagerBase()
        {
           
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                //_LoginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");
                //if (_LoginName.IndexOf(@"\") > 0) _LoginName = string.Empty;

            }
            if(ObjectBase.Container!=null)
                ObjectBase.Container.SatisfyImportsOnce(this);

            if (!string.IsNullOrEmpty(_LoginName))
            {
                _AuthorizationAccount = LoadAuthorizationAccount(_LoginName);
            }

           
        }

        protected virtual Account LoadAuthorizationAccount(string loginName)
        {
            return null;
        }

        protected void ValidateAuthorization(IAccountOwnedEntity account)
        {
            if (!Thread.CurrentPrincipal.IsInRole(Security.CarRentalAdmin))
            {
                if (_AuthorizationAccount != null)
                {
                    if(_LoginName!=string.Empty && account.OwnerAccountId != _AuthorizationAccount.AccountId)
                    {
                        AuthorizationValidationException ex = new AuthorizationValidationException("Attempt to access secure record");
                        throw new FaultException<AuthorizationValidationException>(ex,ex.Message);
                    }
                }
            }
        }

        protected string _LoginName;
        protected Account _AuthorizationAccount=null;

        protected T HandleFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
           
        }

        protected void HandleFaultHandledOperation(Action codeToExecute)
        {
            try
            {
                codeToExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }

        }
    }
}
