import { profileTemplate } from "./profileTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _memesService = undefined;

function initialize(router, renderHandler, memesService) {
    _router = router;
    _renderHandler = renderHandler;
    _memesService = memesService;
}

async function getView(context) {
    let user = context.user;
    let myMemes = [];
    if(user !== undefined){
        myMemes = await _memesService.getMyMemes(user._id);
    }

    let model = {
        memes: myMemes,
        user
    }

    let templateResult = profileTemplate(model);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}