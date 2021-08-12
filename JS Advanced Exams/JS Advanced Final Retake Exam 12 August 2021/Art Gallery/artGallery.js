class ArtGallery {

    constructor(creator) {
        this.creator = creator;

        this.possibleArticles = {
            "picture": 200,
            "photo": 50,
            "item": 250
        };

        this.listOfArticles = [];
        this.guests = [];
    }

    addArticle(articleModel, articleName, quantity) {

        let itsContains = this.possibleArticles[articleModel.toLowerCase()];

        if (!itsContains) {
            throw new Error("This article model is not included in this gallery!");
        }

        let currentArticle = this.listOfArticles.find(x => x.articleModel == articleModel && x.articleName == articleName);

        if (currentArticle !== undefined && currentArticle !== null && currentArticle !== false) {
            currentArticle.quantity += Number(quantity);
        } else {
            let article = {
                articleModel: articleModel.toLowerCase(),
                articleName,
                quantity
            }

            this.listOfArticles.push(article);
        }

        return `Successfully added article ${articleName} with a new quantity- ${quantity}.`;

    }

    inviteGuest(guestName, personality) {

        let isExist = this.guests.find(x => x.guestName == guestName);

        if (isExist !== undefined && isExist !== null && isExist !== false) {
            throw new Error(`${guestName} has already been invited.`);
        }

        let guest = {
            guestName,
            points: points(personality),
            purchaseArticle: 0
        }

        this.guests.push(guest);

        function points(personality) {
            let points = 50;
            if (personality == 'Vip') {
                points = 500;
            } else if (personality == 'Middle') {
                points = 250;
            }
            return points;
        }

        return `You have successfully invited ${guestName}!`;
    }

    buyArticle(articleModel, articleName, guestName) {

        let currentArticle = this.listOfArticles.find(x => x.articleName.toLowerCase() == articleName.toLowerCase()
            && x.articleModel.toLowerCase() == articleModel.toLowerCase());
        let currentGuest = this.guests.find(x => x.guestName == guestName);

        if (currentArticle === undefined || currentArticle === null || currentArticle == false) {
            throw new Error("This article is not found.");
        }

        if (currentArticle.quantity == 0) {
            return `The ${currentArticle.articleName} is not available.`;
        }

        if (currentGuest === undefined || currentGuest === null || currentGuest === false) {
            return "This guest is not invited.";
        }

        let articlePoints = this.possibleArticles[articleModel];

        if (currentGuest.points < articlePoints) {
            return "You need to more points to purchase the article.";
        } else if (currentGuest.points >= articlePoints) {
            currentGuest.points -= articlePoints;
            currentArticle.quantity -= 1;
            currentGuest.purchaseArticle += 1;
        }

        return `${guestName} successfully purchased the article worth ${articlePoints} points.`;
    }

    showGalleryInfo (criteria) {
        let output = '';

        if(criteria == 'article') {
            output += "Articles information:" + "\n";
            for (const model of this.listOfArticles) {
                output += `${model.articleModel} - ${model.articleName} - ${model.quantity}` + "\n";
            }
        } else if (criteria == 'guest') {
            output += "Guests information:" + "\n";
            for (const model of this.guests) {
                output += `${model.guestName} - ${model.purchaseArticle}` + "\n";
            }
        }

        return output.trim();
    }
}

const artGallery = new ArtGallery('Curtis Mayfield');
artGallery.addArticle('picture', 'Mona Liza', 3);
artGallery.addArticle('Item', 'Ancient vase', 2);
artGallery.addArticle('picture', 'Mona Liza', 1);
artGallery.inviteGuest('John', 'Vip');
artGallery.inviteGuest('Peter', 'Middle');
console.log(artGallery.buyArticle('picture', 'Mona Liza', 'John'));
console.log(artGallery.buyArticle('item', 'Ancient vase', 'Peter'));
//console.log(artGallery.buyArticle('item', 'Mona Liza', 'John'));
console.log(artGallery.showGalleryInfo('article'));
console.log(artGallery.showGalleryInfo('guest'));