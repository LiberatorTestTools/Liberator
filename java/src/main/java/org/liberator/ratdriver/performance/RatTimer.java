package org.liberator.ratdriver.performance;

import java.time.Instant;

public class RatTimer implements ITimer  {

    RatTimer(){
        startTime = Instant.now();
        endTime = Instant.now();
        duration = -1;
    }

    public Instant startTime;
    public Instant endTime;
    public long duration;

    /**
     * Start the timer
     */
    @Override
    public void Start() {
        startTime = Instant.now();
        endTime = Instant.now();
        duration = 0;
    }

    /**
     * Stop the timer
     */
    @Override
    public void Stop() {
        endTime = Instant.now();
        duration = endTime.toEpochMilli() - startTime.toEpochMilli();
    }
}