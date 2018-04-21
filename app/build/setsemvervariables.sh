#!/bin/bash
echo "BuildNUmber: $BUILD_BUILDNUMBER"
node index.js -d "$BUILD_BUILDNUMBER"