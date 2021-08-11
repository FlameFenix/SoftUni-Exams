import { AllListingsTemplate } from "./listingsTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _listingsService = undefined;

function initialize(router, renderHandler, listingsService) {
    _router = router;
    _renderHandler = renderHandler;
    _listingsService = listingsService;
}

async function getView(context) {
    let allListings = await _listingsService.getAll();
    let templateResult = AllListingsTemplate(allListings);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}