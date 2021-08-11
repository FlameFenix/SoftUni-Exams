class Story {

    constructor(title, creator) {
        this.title = title;
        this.creator = creator;
        this._comments = [];
        this._likes = [];
        this._firstPerson = '';
    }

    get likes() {
        if (this._likes.length == 0) {
            return `${this.title} has 0 likes`;
        }
        if (this._likes.length == 1) {
            return `${this._firstPerson} likes this story!`;
        }

        return `${this._firstPerson /* should be the first username which likes the story */} and ${this._likes - 1} others like this story!`;

    }

    like(username) {

        if (this._likes.includes(username)) {
            throw new Error("You can't like the same story twice!");
        }
        if (this.creator == username) {
            throw new Error("You can't like your own story!");
        }

        this._likes.push(username);

        if (this._likes.length == 1) {
            this._firstPerson = username;
        }

        return `${username} liked ${this.title}!`;
    }

    dislike(username) {

        if (!this._likes.includes(username)) {
            throw new Error("You can't dislike this story!");
        }
        let indexOfUsername = this._likes.indexOf(username);
        this._likes.splice(indexOfUsername, 1);

        return `${username} disliked ${this.title}`;
    }

    comment(username, content, id) {
        let existingComment = this._comments.find(x => x.id == id);

        if (id === undefined || !existingComment) {
            this._comments.push({
                id: this._comments.length + 1,
                username,
                content,
                replies: []
            });
            return `${username} commented on ${this.title}`;
        }

        existingComment.replies.push(
            {
                id: existingComment.replies.length + 1,
                username,
                content,
            });

        return "You replied successfully";
    }

    toString(sortingType) {
        if (sortingType == 'asc') {
            this._comments.sort((a, b) => a.id - b.id)
            .forEach(x => x.replies.sort((a,b) => a.id - b.id));
        }

        if (sortingType == 'desc') {
            this._comments.sort((a, b) => b.id - a.id)
            .forEach(x => x.replies.sort((a,b) => b.id - a.id));
        }

        if (sortingType == 'username') {
            this._comments.sort((a, b) => a.username.localeCompare(b.username))
            .forEach(x => x.replies.sort((a,b) => a.username.localeCompare(b.username)));
        }

        let output = 'Title: ' + `${this.title}\n` + 'Creator: ' + `${this.creator}\n` + 'Likes: ' + `${this._likes.length}\n` + 'Comments:';
        if(this._comments.length > 0 ) {
            for (const comment of this._comments) {
                output += `\n-- ${comment.id}. ${comment.username}: ${comment.content}`;
                for (const reply of comment.replies) {
                    output += `\n--- ${comment.id}.${reply.id}. ${reply.username}: ${reply.content}`;
                }
            }
        }
        return output;
    };
}


let art = new Story("My Story", "Anny");
art.like("John");
console.log(art.likes);
art.dislike("John");
console.log(art.likes);
art.comment("Sammy", "Some Content");
console.log(art.comment("Ammy", "New Content"));
art.comment("Zane", "Reply", 1);
art.comment("Jessy", "Nice :)");
console.log(art.comment("SAmmy", "Reply@", 1));
console.log()
console.log(art.toString('username'));
console.log()
art.like("Zane");
console.log(art.toString('desc'));
