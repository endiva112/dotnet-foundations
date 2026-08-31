Random random = new Random();
int daysUntilExpiration = random.Next(12);
int discountPercentage = 0;
string message;

if (daysUntilExpiration == 0) {
    message = "Your subscription has expired.";
} else if (daysUntilExpiration == 1) {
    discountPercentage = 20;
    message = $"Your subscription expires within a day! \nRenew now and save {discountPercentage}%!";
} else if (daysUntilExpiration <= 5) {
    discountPercentage = 10;
    message = $"Your subscription expires in {daysUntilExpiration} days. \nRenew now and save {discountPercentage}%!";
} else if (daysUntilExpiration <= 10) {
    message = "Your subscription will expire soon. Renew now!";
} else {
    message = "";
}
Console.Write(message);
