﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupERP.WPF.Comptabiity.ComptabilityWebServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ComptabilityWebServiceReference.IComptabilityService")]
    public interface IComptabilityService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComptabilityService/DoWork", ReplyAction="http://tempuri.org/IComptabilityService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComptabilityService/DoWork", ReplyAction="http://tempuri.org/IComptabilityService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IComptabilityServiceChannel : SupERP.WPF.Comptabiity.ComptabilityWebServiceReference.IComptabilityService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ComptabilityServiceClient : System.ServiceModel.ClientBase<SupERP.WPF.Comptabiity.ComptabilityWebServiceReference.IComptabilityService>, SupERP.WPF.Comptabiity.ComptabilityWebServiceReference.IComptabilityService {
        
        public ComptabilityServiceClient() {
        }
        
        public ComptabilityServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ComptabilityServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ComptabilityServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ComptabilityServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
    }
}
