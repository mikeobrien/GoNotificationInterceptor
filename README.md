ThoughtWorks Go Notification Interceptor
=============

The ThoughtWorks Go Notification Interceptor enables you to hijack Cruise notifications and apply custom formatting before they are passed on. Currently Go does not allow you customize notifications. The interceptor acts as a proxy SMTP server and uses regex and xslt to enable notification customizations. The interceptor runs as a Windows service.

Installation
-------

1. Run the installer and enter the listen port and outgoing smtp server/port.  
2. In the Go server configuration set the email hostname and port to that of the notification interceptor.  
3. If desired, customize the notification stylesheets under "<Program Files>\Go Notification Interceptor". These are called Subject.xslt and Body.xslt.  

Special Thanks
------------

[@phatboyg](http://twitter.com/phatboyg), [@drusellers](http://twitter.com/drusellers), et al for Top Shelf.  
[@eric_daugherty](http://twitter.com/eric_daugherty) for [CSES](http://www.ericdaugherty.com/dev/cses/).  