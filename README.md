<p align="center">
  <img
    src="/logo.png"
    width="125px"
  >
</p>

<h1 align="center">Phantasma Link</h1>

<p align="center">
  A secure and easy to use dapp connector protocol for Phantasma.
</p>

## Commits
[![GitHub last commit](https://img.shields.io/github/last-commit/phantasma-io/PhantasmaLink.svg?style=flat)]()

## Social

### Discord  
[![Discord Chat](https://img.shields.io/discord/404769727634997261.svg)](https://discord.gg/RsKn8EN)  

#### Twitter
[![Twitter Follow](https://img.shields.io/twitter/follow/phantasmachain.svg?style=social)](https://twitter.com/phantasmachain)

## Contents

- [Description](#description)
- [Components](#components)
- [Compatibility](#compatibility)
- [Installation](#installation)
- [Building](#building)
- [Contributing](#contributing)
- [License](#license)

---

## Description

Phantasma Link is a protocol designed to allow dapps to sign transactions and do other on-chain operations on the behalf of users, in a transparent way, without requiring access to the user private key.

To learn more about Phantasma, please read the [White Paper](https://phantasma.io/phantasma_whitepaper.pdf).


## Compatible wallets

List of known wallets that support Phantasma Link protocol.

Wallet 		| Status| Notes
:---------------------- | :------------ |  :------------ | 
Sample Connector 		| Working, version 2 | Not a real wallet, it's a reference implementation
[Poltergeist](https://github.com/phantasma-io/Poltergeist) 		| Working, version 2 support | Phantasma Link support in Windows/Mac/Linux versions only
[Ecto](https://github.com/phantasma-io/Ecto/) 		| Working, version 1 support |  Browser-based
[Phantom](https://github.com/merl111/PhantomWallet) 		| In progress | 
[Pavillion](https://www.pavillionhub.com/) 		| In progress | 

## Protocol specification

If you want to add support to Phantasma Link to your own wallet or other type of application, check the tables below to know which methods you will need to implement.

### Methods
Method 		| Arguments | Example | Notes
:---------------------- | :------------ |  :------------ |  :------------ | 
authorize 		| dappName, version | /authorize/mydapp/2 | Will estabilish a connection between a dapp and a wallet
getAccount 		| platform | /getAccount/phantasma | Will return various fields about the account available in the connected wallet. If multiple accounts are available, this will return the currently selected account.
signData 		| data, signature, platform | /signData/FFAACCEA/Ed25519/phantasma | Will sign binary data with the private key of the current wallet.
signTx 		| chain, script, payload, signature, platform | /signTx/main/RAWSCRIPTHERE/Ed25519/phantasma | Will sign with the private key of the current wallet, and relay it to a node
invokeScript | chain, script | /invokeScript/main/RAWSCRIPTHERE | Will execute a read-only script using onchain data
writeArchive | hash, blockIndex, rawbytes | /writeArchive/FFB9914E6749E7C4A7750FDD2F30890ECDD63021FA661BD85BE3DD108E0EB372/RAWBYTESHERE | Will write an archive chunk to the Phantasma storage

## Javascript librariy

If you are a web dapp developer, you will be able to connect your dapp to Phantasma by downloading the [Javascript](https://github.com/phantasma-io/PhantasmaLink/blob/master/Dapps/www/public/Shared/phantasma.js) library and integrating it with your code.<br>
If you are creating a Unity or .NET based dapp, you will find a Phantasma Link library client [here](https://github.com/phantasma-io/PhantasmaSDK).

### Sample code
Add links to both Phantasma.js and Phantasma.css files (the actual paths may differ, depending on where you install them).
```
  <link href="phantasma/phantasma.css" rel="stylesheet">
  <script src="phantasma/phantasma.js"></script>
```

Add a login button HTML code somewhere in your web page.
```
	<div class="col-md-4 text-center">
		<button type="button" class="btn btn-lg" onclick="loginToPhantasma()">Login</button>
	</div>
```

Add a Javascript function called loginToPhantasma() somewhere in your web page.
```
let mydappName = 'mydapp';
let requiredVersion = 2;
let link = new PhantasmaLink(mydappName); // here we instantiate a Phantasma Link connection

function loginToPhantasma() {
	link.login( function(success) {
		if (success) {
			console.log('Connected to account ' + link.account.address + ' via ' + link.wallet);
		}
	}, requiredVersion);
}
```

Field 		| Notes
:---------------------- | :------------
link.account.name 		| Account name (or anonymous if no name registered)
link.account.address 		| Phantasma address
link.account.avatar 		| Image data (in base64 format, you can use this directly in a <img> element)
link.account.external 		| External blockchain address if available (eg: Ethereum, NEO)
link.token	| Phantasma Link token, save this to persist connection over a session
link.wallet	| Name of the wallet / connector  (eg: Poltergeist, Ecto)


## Contributing

You can contribute to Phantasma with [issues](https://github.com/Phantasma-io/PhantasmaLink/issues) and [PRs](https://github.com/Phantasma-io/PhantasmaLink/pulls). Simply filing issues for problems you encounter is a great way to contribute. Contributing implementations is greatly appreciated.

## License

[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/tterb/atomic-design-ui/blob/master/LICENSEs)

The Phantasma project is released under the MIT license, see `LICENSE.md` for more details.

