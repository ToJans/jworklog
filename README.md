# Jira Worklog

## Extract jira work logs from a server.

As I was working for a client, and unable to get an overview of my work log, I decided to write a small tool that extracts hours of active tasks.

The result is a small tool that gets the hours/day worked for all issues that changed within
the last 2 weeks.

## Usage

### Disclaimer

This was a quick & dirty hack to check whether my stuff works. No guarantees whatsoever are offered.

### License

[WTFPL – Do What the Fuck You Want to Public License](http://www.wtfpl.net/)

### Installation

Download and unzip a compiled version [here](download/jworklog.zip) or clone it from the github repo and build using visual studio:

    git clone https://github.com/ToJans/jworklog.git


### Setup

Run jworklog.exe.

The first time it will generate a config file and tell you where to find it.

The idea is you edit this file, and adjust it.

This is the default output: 

        No config found; created one.
        Please check your config file at :
                C:\Users\Joe\AppData\Roaming\jirasettings.json

The default file looks like this:

        {
          "Username": "jira username",
          "Password": "jira password",
          "ServerUrl": "http://somejiraurl.com",
          "BaseJQL": "updated > -2w"
        }

Change this with your settings, and run the app again.

The `BaseJQL` can be changed to anything you'd like it to be; I'm not an expert on `JQL`.

### Usage

When you run the app, it will query jira issues using the `BaseJQL` , and get worklogs for all issues returned 2 times:

- First list: user, date, issue, hourslogged
- Second list: user, date, hourslogged

Here is some example output:

    Querying Jira based on settings from config file.
    Please check your config file at :
        C:\Users\Joe\AppData\Roaming\jirasettings.json
    
    tojans	17/03/2015	CYPRES-355	8
    tojans	18/03/2015	CYPRES-355	8,5
    tojans	19/03/2015	CYPRES-355	9,75
    tojans	20/03/2015	CYPRES-355	7,75
    tojans	23/03/2015	CYPRES-355	10
    tojans	24/03/2015	CYPRES-355	10
    tojans	25/03/2015	CYPRES-355	9
    tojans	26/03/2015	CYPRES-355	8
    tojans	27/03/2015	CYPRES-355	9
    tojans	1/04/2015	CYPRES-355	9
    tojans	2/04/2015	CYPRES-355	8
    tojans	3/04/2015	CYPRES-355	8
    tojans	7/04/2015	CYPRES-355	8
    tojans	8/04/2015	CYPRES-355	10,75
    tojans	9/04/2015	CYPRES-355	10,25
    tojans	8/06/2015	CYPRES-1338	1
    tojans	9/06/2015	CYPRES-1338	7
    tojans	10/06/2015	CYPRES-1338	10
    tojans	15/06/2015	CYPRES-1338	4
    tojans	16/06/2015	CYPRES-1338	8
    tojans	18/06/2015	CYPRES-1338	1,25
    tojans	22/06/2015	CYPRES-1338	8
    tojans	23/06/2015	CYPRES-1338	0,5
    tojans	23/06/2015	CYPRES-1419	7,5
    tojans	24/06/2015	CYPRES-1419	8,5
    tojans	13/07/2015	CYPRES-1419	7
    tojans	14/07/2015	CYPRES-1419	2,5
    tojans	14/07/2015	CYPRES-1469	0,25
    tojans	14/07/2015	CYPRES-1470	3,25
    tojans	14/07/2015	CYPRES-1560	2
    tom.leathermaker	11/12/2014	CYPRES-156	1
    tom.leathermaker	7/05/2015	CYPRES-156	0,5
    tom.leathermaker	9/07/2015	CYPRES-156	7
    tom.leathermaker	10/07/2015	CYPRES-156	4

    tojans	17/03/2015	8
    tojans	18/03/2015	8,5
    tojans	19/03/2015	9,75
    tojans	20/03/2015	7,75
    tojans	23/03/2015	10
    tojans	24/03/2015	10
    tojans	25/03/2015	9
    tojans	26/03/2015	8
    tojans	27/03/2015	9
    tojans	1/04/2015	9
    tojans	2/04/2015	8
    tojans	3/04/2015	8
    tojans	7/04/2015	8
    tojans	8/04/2015	10,75
    tojans	9/04/2015	10,25
    tojans	8/06/2015	1
    tojans	9/06/2015	7
    tojans	10/06/2015	10
    tojans	15/06/2015	4
    tojans	16/06/2015	8
    tojans	18/06/2015	1,25
    tojans	22/06/2015	8
    tojans	23/06/2015	8
    tojans	24/06/2015	8,5
    tojans	13/07/2015	7
    tojans	14/07/2015	8
    tom.leathermaker	11/12/2014	1
    tom.leathermaker	7/05/2015	0,5
    tom.leathermaker	9/07/2015	7
    tom.leathermaker	10/07/2015	4
