export const isSitecoreExperienceEditor = () => {
    const bodyClass = document.getElementsByTagName('body')[0].className;
    if (bodyClass && bodyClass.indexOf('experience-editor') > -1)
    {
        return true;
    }
    return false;
}