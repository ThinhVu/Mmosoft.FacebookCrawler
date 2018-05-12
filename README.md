
I. Solution hierarchy
1. Lib project
- Contain Facebook SDK which support only for crawler data.
- Contain Utilities: 
	+ HttpHandler: Create and send POST, GET request and receive response from FB.
	+ HtmlHelper: Fault tolerant HTML parser (wrapper HtmlAgilityPack)
	+ CompiledRegex: Contain compiled regex map which will process string to get fb info.
	+ Localization: contain localization strings, aim to support multiple language. At the moment, only suppor En
	+ StringHelper: contain methods which word with strings.
	+ WorkLog: contain methods which help log process of Facebook SDK.
	+ XpathErrorLog: contain methods which help log xpath error to file for later analysis.
- Contain Models:
	+ User Models: User infomation
	+ Page Models: Page information
	+ Group Models: Group infomation
- Exceptions:
	+ Contain some Exception classes.
		
2. FbFarm control center
A console application which will using Facebook SDK lib to crawler data from FB.

Classes:	
	+ FbFarmer: Facebook farmers in a farm, a 'human' wrapper for Facebook SDK
	+ ProductStore: a house contain product after harvest.
	+ SeedStore: a house contain seed to supply for farmers.
	+ FacebookFarm: A farm which contains SeedStore, ProductStore and a lot of farmers.
	
Folders:
	+ Bin\Data : Contain data which control center need to use.	
	+ Localization folder : Contain any language file. These files contains display text in Facebook for any language using for extract value.		
	+ Farmers.txt : Contains Facebook accounts which use for Facebook sdk.
	+ Seeds : Contains Facebook ids or alias will be crawler by FbFarmer (FB SDK wrapper).
	+ OldSeeds : To remove duplicate crawl, each seed after crawl by FbFamer will be store at this file.
	+ Unprocessed Farmers : Leaked FB account get from internet. These file will be process and add to Farmers.txt file (only if user credential can use).
	- Bin\Session : Contain log information about FbFarmer work. Each farmer when run will store its work process these folders.
		+ Session folder : will be create in format : Year_Month_Day_[x] with [x] is number of times FbFarm Control center run in a days.
			Suppor the first time control center run, Folder y_mm_d_0 will be create and all FbFarmer will store its work in this folder.
			The seconds run, folder y_mm_d_1 will be create.
			+ FbFarmer folder will be create for each FbFamer.
				+ Log : Contain log of this farmer, if farmer write > 10000 line of text to one file, another file will be create.
				+ Error : Contain Xpath errors file. Each error will be store in one file.				
				
	
