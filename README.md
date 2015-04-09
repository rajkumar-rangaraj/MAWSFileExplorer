Azure Web Apps Disk Usage - MAWSFileExplorer 
==========
Azure Web App has a disk space quota limitation, ref: http://azure.microsoft.com/en-us/pricing/details/websites/. If we run into disk quota issue, we have to use Kudu's Debug console and navigate through all folders to understand which files are using high disk space.

Azure Web Apps Disk Usage Site Extension is a web based File Explorer which lists all folders for Azure website with its size. It provides detailed information about files and folders name, size, number of files, last date modified and percentage of disk usage. This tool provides a tree like folder view for easier parsing.
