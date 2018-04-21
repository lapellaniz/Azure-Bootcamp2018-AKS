# 

## Setup VSTS repository

In VSTS create a new repository for the source code.

Clone the git repository to a local working directory.

## Init GitVersion

Run `GitVersion init` and follow the prompts.

1. Select 2 for GitFlowHub
2. Increment based on branch config every commit (continuous deployment mode)

## Copy application lab files

Copy [application lab files](../../app) into the repository and push.

---
[lab](00-lab-environment.md) > [setup](01-setup.md) > [docker](02-docker.md) > [cicd](03-cicd.md) > [cofig](04-configuration.md) > [logging](05-logging.md) > [readiness](06-readiness.md)