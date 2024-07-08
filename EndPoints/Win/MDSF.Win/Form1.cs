using static MDSF.Customer.Features.Registration.Registration;
using static MDSF.Operator.Features.ApplyService.ApplyService;

namespace MDSF.Win
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var endPoint = new CreateCustomerWinEndpoint();
            var result = await endPoint.CreateCustomer(new CreateCustomerRequest
            (
                5565665,
                "hossein",
                new Customer.Models.ValueObjects.CustomerInfo("name"),
                 new Customer.Models.ValueObjects.Account
                (
                    "10",
                    1245798
                )
            ), CancellationToken.None);

            //var endPoint = new ApplyServiceWinEndpoint();
            //var result = await endPoint.ApplyService(new ApplyServiceRequest
            //    (6464644, 46444), CancellationToken.None);
        }
    }
}
