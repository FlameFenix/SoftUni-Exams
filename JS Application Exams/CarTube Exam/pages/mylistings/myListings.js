import { myListingsTemplate } from "./mylistingsTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _listingsService = undefined;

function initialize(router, renderHandler, listingsService) {
    _router = router;
    _renderHandler = renderHandler;
    _listingsService = listingsService;
}

async function getView(context) {
    let user = context.user;
    let myListings = [];
    if(user !== undefined){
        myListings = await _listingsService.getMyListings(user._id);
    }

    let model = {
        listings: myListings,
        user
    }

    let templateResult = myListingsTemplate(model);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}