
<h1 align="center">SecureLib</h1>

<p align="center">
  <img alt="Github top language" src="https://img.shields.io/github/languages/top/devgalop/lrn.securelib?color=56BEB8">
  <img alt="Github language count" src="https://img.shields.io/github/languages/count/devgalop/lrn.securelib?color=56BEB8">
  <img alt="Repository size" src="https://img.shields.io/github/repo-size/devgalop/lrn.securelib?color=56BEB8">
  <img alt="License" src="https://img.shields.io/github/license/devgalop/lrn.securelib?color=56BEB8">
  <!-- <img alt="Github issues" src="https://img.shields.io/github/issues/{{github}}/{{repository}}?color=56BEB8" /> -->
  <!-- <img alt="Github forks" src="https://img.shields.io/github/forks/{{github}}/{{repository}}?color=56BEB8" /> -->
  <!-- <img alt="Github stars" src="https://img.shields.io/github/stars/{{github}}/{{repository}}?color=56BEB8" /> -->
</p>

<!-- Status -->

<h4 align="center"> 
	🚧  SecureLib 🚀 Under construction...  🚧
</h4> 

<hr>

<p align="center">
  <a href="#dart-about">About</a> &#xa0; | &#xa0; 
  <a href="#sparkles-features">Features</a> &#xa0; | &#xa0;
  <a href="#rocket-technologies">Technologies</a> &#xa0; | &#xa0;
  <a href="#white_check_mark-requirements">Requirements</a> &#xa0; | &#xa0;
  <a href="#checkered_flag-starting">Starting</a> &#xa0; | &#xa0;
  <a href="https://github.com/devgalop" target="_blank">Author</a>
</p>

<br>

## :dart: About SecureLib

Describe your project

## :sparkles: Features

:heavy_check_mark: Aes encryption - decryption\
:heavy_check_mark: RSA encryption - decryption\
:heavy_check_mark: JWT tokens

## :rocket: Technologies

The following tools were used in this project:

- [.Net 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Vs Code](https://code.visualstudio.com/download)


## :white_check_mark: Requirements

Before starting :checkered_flag:, you need to have [Git](https://git-scm.com), [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) and [Docker](https://www.docker.com/) installed.

## :checkered_flag: Starting

```bash
# Clone this project
$ git clone https://github.com/devgalop/lrn.securelib

# Access
$ cd {ROOT_FOLDER}

# Install dependencies
$ dotnet restore ./lrn.devgalop.securelib.WebApi/lrn.devgalop.securelib.WebApi.csproj

# Build docker image
$ docker build -t securelib:development --progress=plan --no-cache .

# Run docker-compose
$ docker-compose -f ./Docker/docker-compose-development.yml up -d

# The server will initialize in the <http://localhost:5200>
```

<a href="#top">Back to top</a>