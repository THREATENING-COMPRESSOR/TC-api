using TC.Domain.Catalog;

namespace TC.Domain.Tests;

[TestClass]
public class RatingTests
{
    [TestMethod]
    public void Can_Create_New_Rating()
    {
        // arrange
        var rating = new Rating(1, "Mike", "Great fit!");

        // act (empty)

        // assert
        Assert.AreEqual(1, rating.Stars);
        Assert.AreEqual("Mike", rating.UserName);
        Assert.AreEqual("Great fit!", rating.Review);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Cannot_Create_New_Rating_Without_Valid_Stars()
    {
        // arrange
        var rating = new Rating(0, "Mike", "Great fit!");

    }

    [TestMethod]
    public void Can_Create_New_Item()
    {
        // arrange
        var item = new Item("Chicken", "Is chicken", "KFC", 15.99m);

        // assert
        Assert.AreEqual("Chicken", item.Name);
        Assert.AreEqual("Is chicken", item.Description);
        Assert.AreEqual("KFC", item.Brand);
        Assert.AreEqual(15.99m, item.Price);

    }

        [TestMethod]

    public void Can_Create_Add_Rating()
    {
        // arrange
        var item = new Item("Chicken", "Is chicken", "KFC", 15.99m);
        var rating = new Rating(5, "Dave", "The chicken is dry");

        // add to item
        item.AddRating(rating);

        // assert
        Assert.AreEqual(item.Ratings[0], rating);
    }
}