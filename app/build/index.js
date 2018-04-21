var semver = require('./version');
var args = require('yargs')
    .option('defaultVersion', {
        alias: 'd',
        default: '1.0.0'
    }).argv;
console.log(semver.tag);
if (semver.tag == null) {
    console.log("##vso[task.setvariable variable=DOCKER_VERSION;]"+args.defaultVersion);
    console.log("##vso[task.setvariable variable=DOCKER_LATEST_VERSION;]"+args.defaultVersion);
    console.log("##vso[task.setvariable variable=DOCKER_LINUX_VERSION;]"+args.defaultVersion);
}
else {
    console.log("##vso[task.setvariable variable=DOCKER_VERSION;]" + semver.tag);
    console.log("##vso[task.setvariable variable=DOCKER_LATEST_VERSION;]" + semver.tag);
    console.log("##vso[task.setvariable variable=DOCKER_LINUX_VERSION;]" + semver.tag);
}