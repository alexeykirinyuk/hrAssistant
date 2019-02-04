package ru.tinkoff.controllers;

import org.springframework.web.bind.annotation.*;
import ru.tinkoff.models.Job;
import ru.tinkoff.services.JobService;

@RestController
@RequestMapping("/api/jobs")
public final class JobRestController {

    private final JobService jobService;

    public JobRestController() {
        this.jobService = new JobService();
    }

    @GetMapping("/{code}")
    public Job getJob(@PathVariable String code) {
        return this.jobService.getJob(code);
    }
}
