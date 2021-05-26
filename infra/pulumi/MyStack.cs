namespace liontrust
{
    using Pulumi;
    using Pulumi.Kubernetes.Core.V1;
    using Pulumi.Kubernetes.Helm;
    using Pulumi.Kubernetes.Helm.V3;

    class MyStack : Stack
    {
        [Output]
        public Output<string> ResourceGroupName { get; set; }

        [Output]
        public Output<string> ClusterName { get; set; }

        [Output]
        public Output<string> Kubeconfig { get; set; }

        public MyStack()
        {
            var myConfig = new MyConfig();
            var myCluster = new MyCluster(myConfig);

            this.ResourceGroupName = myCluster.ResourceGroupName;
            this.ClusterName = myCluster.ClusterName;
            this.Kubeconfig = myCluster.Kubeconfig;
        }
    }
}
