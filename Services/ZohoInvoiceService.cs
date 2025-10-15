using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace PRN221_Project_02.Services
{
    public class ZohoInvoiceService
    {
        public void OpenAndFillZohoInvoice(string productName, string warehouseName, string quantity, string supplierName,
            string movementDate, string status, string pic, string supAddress, string supPhone,
            string supEmail, int movementId, decimal? price)
        {
            var options = new EdgeOptions();

            IWebDriver driver = new EdgeDriver(options);

            try
            {
                driver.Navigate().GoToUrl("https://www.zoho.com/invoice/free-invoice-generator.html");

                // Sử dụng WebDriverWait để đợi phần tử cần điền xuất hiện
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Điền tiêu đề hóa đơn
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("title")));
                var titleElement = driver.FindElement(By.Id("title"));
                titleElement.Clear();
                titleElement.SendKeys("Import Invoice");

                // Điền tên cong ty
                var companyName = driver.FindElement(By.Id("address1"));
                companyName.SendKeys("FreshTrack");

                // Điền tên người phụ trách
                if (!string.IsNullOrEmpty(pic))
                {
                    var picElement = driver.FindElement(By.Id("custName"));
                    picElement.SendKeys(pic);
                }

                // Điền dia chi cong ty
                var companyAdd = driver.FindElement(By.Id("address2"));
                companyAdd.SendKeys("FPT University");

                var companyCity = driver.FindElement(By.Id("address3"));
                companyAdd.SendKeys("Hanoi");
                
                
                var companycountry = driver.FindElement(By.Id("companyCountry"));
                companyAdd.SendKeys("VietNam");



                //ten supplier 
                
                var clientAddress = driver.FindElement(By.Id("billingAddress1"));
                clientAddress.SendKeys(supplierName);


                var supplierAddress = driver.FindElement(By.Id("billingAddress2"));
                supplierAddress.SendKeys(supAddress.ToString());


                var supplierPhone = driver.FindElement(By.Id("billingAddress3"));
                supplierPhone.SendKeys(supPhone.ToString());

                

                // Điền số hóa đơn
                var invoiceNumber = driver.FindElement(By.Id("invNumber"));
                invoiceNumber.Clear();
                invoiceNumber.SendKeys(movementId.ToString());


                // Điền ngày hóa đơn
                var invoiceDate = driver.FindElement(By.Id("invoiceDate"));
                invoiceDate.Clear();
                invoiceDate.SendKeys(movementDate);

                 var dueDate = driver.FindElement(By.Id("dueDate"));
                dueDate.Clear();
                dueDate.SendKeys(movementDate);



                // Điền tên hang
                var proName = driver.FindElement(By.Id("itemDesc.1"));
                proName.Clear();
                proName.SendKeys(productName);

                
                // Điền số lượng sản phẩm
                var proQuan = driver.FindElement(By.Name("itemQty.1"));
                proQuan.Clear();
                proQuan.SendKeys(Keys.Control + "a");
                proQuan.SendKeys(Keys.Delete);
                proQuan.SendKeys(quantity);

                // Điền đơn giá
                var itemRate = driver.FindElement(By.Name("itemRate.1"));
                itemRate.Clear();
                itemRate.SendKeys(Keys.Control + "a");
                itemRate.SendKeys(Keys.Delete);
                itemRate.SendKeys(price.ToString());



                var itemTax = driver.FindElement(By.Name("itemTax1.1"));
                itemTax.Clear();
                itemTax.SendKeys(Keys.Control + "a");
                itemTax.SendKeys(Keys.Delete);
                itemTax.SendKeys("0"); 

                

                

                Console.WriteLine("Invoice filled and preview opened successfully.");

                // Chờ đợi để kiểm tra kết quả, nhấn phím bất kỳ để tiếp tục
                Console.WriteLine("Press any key to close the browser...");
                Console.ReadKey();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("An error occurred: Element not found - " + ex.Message);
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine("An error occurred: Timeout waiting for element - " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                driver.Quit(); // Đóng trình duyệt sau khi nhấn phím
            }
        }
    }
}
