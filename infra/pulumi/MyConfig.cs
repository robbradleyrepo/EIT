namespace liontrust
{
    using Pulumi;
    using Pulumi.Random;
    using Pulumi.Tls;

    public class MyConfig
    {
        public string K8SVersion { get; set; }
        public int NodeCount { get; set; }
        public string NodeSize { get; set; }
        public PrivateKey GeneratedKeyPair { get; set; }
        public string AdminUserName { get; set; }
        public Output<string> Password { get; set; }
        public Output<string> SshPublicKey { get; set; }

        public MyConfig()
        {
            var cfg = new Config();

            this.K8SVersion = cfg.Get("k8sVersion") ?? "1.18.14";
            this.NodeCount = cfg.GetInt32("nodeCount") ?? 1;
            this.NodeSize = cfg.Get("nodeSize") ?? "Standard_D2_v2";

            this.GeneratedKeyPair = new PrivateKey("ssh-key", new PrivateKeyArgs
            {
                Algorithm = "RSA",
                RsaBits = 4096
            });

            this.AdminUserName = cfg.Get("adminUserName") ?? "testuser";

            var pw = cfg.Get("password");
            if (pw == null)
            {
                this.Password = this.GenerateRandomPassword();
            }
            else
            {
                this.Password = Output.Create(pw);
            }

            var sshPubKey = cfg.Get("sshPublicKey");
            if (sshPubKey == null)
            {
                this.SshPublicKey = this.GeneratedKeyPair.PublicKeyOpenssh;
            }
            else
            {
                this.SshPublicKey = Output.Create(sshPubKey);
            }
        }

        private Output<string> GenerateRandomPassword()
        {
            var pw = new RandomPassword("pw", new RandomPasswordArgs
            {
                Length = 20,
                Special = true
            });
            return pw.Result;
        }
    }
}
