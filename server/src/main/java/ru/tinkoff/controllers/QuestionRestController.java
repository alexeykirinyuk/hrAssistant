package ru.tinkoff.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import ru.tinkoff.exceptions.JobNotFoundException;
import ru.tinkoff.models.*;
import ru.tinkoff.services.JobService;
import ru.tinkoff.services.QuestionService;

import java.util.Collection;

@Controller
@RequestMapping("/api/questions")
public final class QuestionRestController {
    private final QuestionService questionService;

    public QuestionRestController() {
        this.questionService = new QuestionService();
    }

    @GetMapping("/{jobCode}")
    public Collection<Question> getAll(@PathVariable String jobCode) {
        return this.questionService.getAll(jobCode);
    }
}
