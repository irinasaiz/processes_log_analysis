# processes_log_analysis
Service that analyses processes logs

Request:
1. Parse the CSV log file.
2. Identify each job or task and track its start and finish times.
3. Calculate the duration of each job from the time it started to the time it finished.
4. Produce a report or output that:
• Logs a warning if a job took longer than 5 minutes.
• Logs an error if a job took longer than 10 minutes.



Apart from the current implementation, what I would have done given more time:
* Return failure if log file is malformed in various other cases:
    * missing fields
    * pid appearing multiple times
    * pid having end time before start time
* Make code mode modular - with the current implementation, the tests are end to end, the private methods are not tested.
* The output is not tested either, just the success/failure.
* Store the jobs in a dictionary, indexed by pid, so that retrieval of information is faster/cleaner.
  I could also store the duration of the job the moment I populate the dictionary, 
  without the need to compute it each time I need info about the job (assuming this will be done repeatedly)
  Right now I iterate through the list and make difference between end and start.
