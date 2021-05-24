using Pulumi;
using Azure = Pulumi.Azure;
class WebServerStack : Stack
{
    public WebServerStack()
    {
        var resourceGroup = new Azure.Core.ResourceGroup("exampleResourceGroup", new Azure.Core.ResourceGroupArgs
        {
            Location = "UK South",
        });
        var virtualNetwork = new Azure.Network.VirtualNetwork("exampleVirtualNetwork", new Azure.Network.VirtualNetworkArgs
        {
            AddressSpaces =
            {
                "10.0.0.0/16",
            },
            Location = resourceGroup.Location,
            ResourceGroupName = resourceGroup.Name,
        });
        var subnet = new Azure.Network.Subnet("exampleSubnet", new Azure.Network.SubnetArgs
        {
            ResourceGroupName = resourceGroup.Name,
            VirtualNetworkName = virtualNetwork.Name,
            AddressPrefixes =
            {
                "10.0.2.0/24",
            },
        });
        var publicIp = new Azure.Network.PublicIp("liontrust_public_ip", new Azure.Network.PublicIpArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AllocationMethod = "Dynamic" });

        var networkInterface = new Azure.Network.NetworkInterface("exampleNetworkInterface", new Azure.Network.NetworkInterfaceArgs
        {
            Location = resourceGroup.Location,
            ResourceGroupName = resourceGroup.Name,
            IpConfigurations =
            {
                new Azure.Network.Inputs.NetworkInterfaceIpConfigurationArgs
                {
                    Name = "internal",
                    SubnetId = subnet.Id,
                    PrivateIpAddressAllocation = "Dynamic",
                    PublicIpAddressId = publicIp.Id
                },
            },
        });
        new Azure.Compute.WindowsVirtualMachine("liontrust", new Azure.Compute.WindowsVirtualMachineArgs
        {
            ComputerName = "liontrust-qa",
            ResourceGroupName = resourceGroup.Name,
            Location = resourceGroup.Location,
            Size = "Standard_D4s_v4",
            AdminUsername = "adminuser",
            AdminPassword = "P@$$w0rd1234!",
            NetworkInterfaceIds =
            {
                networkInterface.Id,
            },
            OsDisk = new Azure.Compute.Inputs.WindowsVirtualMachineOsDiskArgs
            {
                Caching = "ReadWrite",
                StorageAccountType = "Standard_LRS",
            },
            SourceImageReference = new Azure.Compute.Inputs.WindowsVirtualMachineSourceImageReferenceArgs
            {
                Publisher = "MicrosoftWindowsServer",
                Offer = "WindowsServer",
                Sku = "2016-Datacenter",
                Version = "latest",
            },
        });
    }
}