using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewMoney.Models;
using PayPal.Api;


namespace NewMoney.Controllers
{
    public class PayPalController : Controller
    {
        public ApplicationDbContext MyContext = new ApplicationDbContext();
        private Payment payment;
        // GET: PayPal
        public ActionResult Index()
        {
            return View();
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            string boughBit = "Bit";
            int numberOfBit = 1;

            var listItems = new ItemList() { items = new List<Item>() };

            var Item = new Item() { name = boughBit, currency = "USD", quantity = numberOfBit.ToString(), sku = "sku", price = (numberOfBit * 100).ToString() };

            listItems.items.Add(Item);

            var payer = new Payer() { payment_method = "paypal" };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = Item.price,

            };

            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),
                details = details

            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Testing buying bit from EasyMoney",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);

        }

        //Create ExcutePayment method 
        private Payment ExcutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            payment = new Payment() { id = paymentId };

            return payment.Execute(apiContext, paymentExecution);
        }

        //Create PaymentWithPaypal method
        public ActionResult PaymentWithPaypal()
        {
            // Getting context from the paypal base on clientId and clientSecret for payment 
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerId"];

                if (string.IsNullOrEmpty(payerId))
                {

                    // Creating a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayPal/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //Get links returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);

                }
                else
                {
                    //this one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executedPayment = ExcutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("PaypalFail");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View("PaypalFail");
                //PaypalLogger.Log("Error: " + ex.Message);
                //return View("PaypalFail");
            }
            ApplicationUser currentUser = MyContext.Users.Where(u => u.Email == @User.Identity.Name).FirstOrDefault();
            currentUser.Bits += 10000;
            MyContext.SaveChanges();


            return View("PaypalSuccess");
        }

    }
}