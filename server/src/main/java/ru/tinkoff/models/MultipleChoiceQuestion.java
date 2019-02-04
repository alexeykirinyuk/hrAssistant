package ru.tinkoff.models;

import ru.tinkoff.helpers.Guard;

import java.util.Collection;

public class MultipleChoiceQuestion extends Question {
    private final Collection<Option> choices;

    public MultipleChoiceQuestion(String title, boolean required, Collection<Option> choices) {
        super(title, required);

        Guard.IsNotNull(choices, "choices");
        this.choices = choices;
    }

    @Override
    public QuestionType getQuestionType() {
        return QuestionType.MultipleChoice;
    }

    public Collection<Option> getChoices() {
        return choices;
    }
}
