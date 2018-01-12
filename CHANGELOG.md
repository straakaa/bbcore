# Changelog

## [unreleased]

### Fixed

* Exit by Ctrl+C improved
* Missing resources after rebuild
* Example files list after rebuild

## 0.4.0

### Added

* Parsing commandline and running commands implemented. Interactive command is now true default command, so all its parameters works without specifying it too.
* Listening port in iteractive mode selectable from command line.
* New --verbose parameter for interactive commands (it logs all first chance exceptions). Shortened output without --verbose.

### Fixed

* Crashes when started in directory without package.json. Automatically compiling index.ts(x)

## 0.3.1

### Fixed

* Js file is added to compilation when locally importing its d.ts file. When recompiling too.

## 0.3.0

### Added

* Js file is added to compilation when locally importing its d.ts file.

## 0.2.0

### Added

* Support for bobril.additionalResourcesDirectory
* Added workaround for clients requesting files with wrong casing
* New console message about compilation starting

## 0.1.0

### Added

* Linux version!
* Logging file count in memory after compilation.

### Fixed

* Optimized speed of searching in SourceMaps
* Fixed missing en-us.js

## 0.0.1

### Added

* Changelog
* First version released through GitHub releases.

## 0.0.0